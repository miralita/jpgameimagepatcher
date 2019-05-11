using deltaq.BsDiff;
using DiscUtils;
using DiscUtils.Fat;
using SharedTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatchBuilder {
    class PatchProcessor {
        string[] sourceImages;
        string[] patchedImages;
        int filesCount = 0;
        int processedFiles = 0;
        int patchedFiles = 0;
        public PatchContainer Patch { get; private set; }
        public string LogoPath { get; set; }
        public string SourceFolder { get; set; }
        public string PatchedFolder { get; set; }
        public string Description { get; set; }

        public delegate void StrDataDelegate(string data);
        public event StrDataDelegate UppendLog = delegate{};

        public delegate void IntDataDelegate(int data);
        public event IntDataDelegate UpdateProgress = delegate { };

        public delegate void EmptyDelegate();
        public event EmptyDelegate Finished = delegate { };
        public event StrDataDelegate FinishedError = delegate { };

        public void Run() {
            new Thread(() => {
                Thread.CurrentThread.IsBackground = true;
                try {
                    filesCount = CountFiles();
                } catch (Exception ex) {
                    Console.WriteLine(ex.StackTrace);
                    FinishedError(ex.Message);
                    return;
                }
                UppendLog($"Total files to process: {filesCount}");
                if (filesCount == 0) {
                    FinishedError("Can't list files in images");
                    return;
                }
                try {
                    MakePatch();
                } catch (Exception ex) {
                    Console.WriteLine(ex.StackTrace);
                    FinishedError(ex.Message);
                    return;
                }
                UppendLog($"==================================================\r\nTotal files: {filesCount}, patched files: {patchedFiles}");
                Thread.Sleep(3000);
                Finished();

            }).Start();
        }

        private void MakePatch() {
            int imageN = 0;
            Patch = new PatchContainer();
            if (!string.IsNullOrEmpty(LogoPath) && File.Exists(LogoPath)) {
                Patch.LogoImage = File.ReadAllBytes(LogoPath);
            }
            Patch.Description = Description;
            foreach (var img in sourceImages) {
                UppendLog("======================================================");
                UppendLog($"Working on {img}...");
                UppendLog("~~~~~~~~~~~");
                var pimg = patchedImages.First(fn => Path.GetFileName(fn).ToLower() == Path.GetFileName(img).ToLower());
                if (string.IsNullOrEmpty(pimg)) {
                    UppendLog($"Can't find patched image for {img}");
                    continue;
                }
                imageN++;
                using (var srcDisk = VirtualDisk.OpenDisk(img, FileAccess.Read)) {
                    using (var patchedDisk = VirtualDisk.OpenDisk(pimg, FileAccess.Read)) {
                        using (var srcFat = new PC98FatFileSystem(srcDisk.Content, new FileSystemParameters {
                            SectorSize = srcDisk.SectorSize,
                            FileNameEncoding = Encoding.GetEncoding("shift-jis")
                        })) {
                            using (var patchedFat = new PC98FatFileSystem(patchedDisk.Content, new FileSystemParameters {
                                SectorSize = patchedDisk.SectorSize,
                                FileNameEncoding = Encoding.GetEncoding("shift-jis")
                            })) {
                                CompareImages(imageN, img, pimg, srcFat, patchedFat);
                            }
                        }
                    }
                }
            }
        }

        private void CompareImages(int imageN, string img, string pimg, PC98FatFileSystem srcFat, PC98FatFileSystem patchedFat) {
            var files = srcFat.GetFiles(@"\");
            foreach (var file in files) {
                using (var srcfh = srcFat.OpenFile(file, FileMode.Open)) {
                    if (!patchedFat.FileExists(file)) {
                        throw new FileNotFoundException($"Can't find file {file} from source image {img} in the patched image {pimg}");
                    }
                    using (var dstfh = patchedFat.OpenFile(file, FileMode.Open)) {
                        var fileInfo = new PatchedFile {
                            OriginalMd5Sum = Utils.Md5sum(srcfh),
                            ImageNum = imageN,
                            Name = file
                        };
                        var patchedMd5 = Utils.Md5sum(dstfh);
                        if (fileInfo.OriginalMd5Sum != patchedMd5) {
                            UppendLog($"[PATCH] {file}");
                            fileInfo.Action = PatchAction.Patch;
                            using (var ms = new MemoryStream()) {
                                BsDiff.Create(Utils.DumpStream(srcfh), Utils.DumpStream(dstfh), ms);
                                fileInfo.Patch = ms.ToArray();
                            }
                            patchedFiles++;
                        } else {
                            UppendLog($"[KEEP] {file}");
                            fileInfo.Action = PatchAction.Original;
                        }
                        Patch.Add(fileInfo);
                        processedFiles++;
                        ReportProgress();
                    }
                }
            }
        }

        private void ReportProgress() {
            var progress = (processedFiles * 100) / filesCount;
            UpdateProgress(progress);
        }

        private int CountFiles() {
            int cnt = 0;
            foreach (var img in patchedImages) {
                Console.WriteLine(img);
                Debug.WriteLine(img);
                using (var disk = VirtualDisk.OpenDisk(img, FileAccess.Read)) {
                    var param = new FileSystemParameters();
                    param.SectorSize = disk.SectorSize;
                    //param.SectorSize = 1024;
                    param.FileNameEncoding = Encoding.GetEncoding("shift-jis");
                    using (var fat = new PC98FatFileSystem(disk.Content, param)) {
                        var files = fat.GetFiles(@"\");
                        cnt += files.Length;
                    }
                }
            }
            return cnt;
        }

        internal void Save(string fileName) {
            Patch.Save(fileName);
        }

        internal bool AddSource(string folderName, out string err) {
            err = "";
            var exts = new string[]{ ".xdf", ".dim", ".hdm" };
            var files = Directory.GetFiles(folderName).Where(fname => exts.Any(ext => ext == Path.GetExtension(fname.ToLower()))).OrderBy(fname => fname).ToArray();
            if (files.Length == 0) {
                err = "Can't find XDF, HDM or DIM images in source folder";
                return false;
            }
            var orig_sf = SourceFolder;
            var orig_files = sourceImages;
            SourceFolder = folderName;
            sourceImages = files;
            if (!string.IsNullOrEmpty(PatchedFolder)) {
                if (!AddPatched(PatchedFolder, out err)) {
                    SourceFolder = orig_sf;
                    sourceImages = orig_files;
                    return false;
                }
            }
            return true;
        }

        private bool IsSupportedType(string file) {
            var ext = Path.GetExtension(file).ToLower();
            return ext == "xdf" || ext == "dim" || ext == "hdm";
        }

        internal bool AddPatched(string folderName, out string err) {
            err = "";
            var files = Directory.GetFiles(folderName).Where(file => sourceImages.Any(sf => Path.GetFileName(sf.ToLower()) == Path.GetFileName(file.ToLower()))).ToArray();
            if (files.Length == 0) {
                err = "Can't find suitable patched images";
                return false;
            }
            patchedImages = files;
            PatchedFolder = folderName;
            return true;
        }
    }
}
