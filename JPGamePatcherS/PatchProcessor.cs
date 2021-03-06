﻿using deltaq.BsDiff;
using DiscUtils;
using DiscUtils.Fat;
using SharedTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JPGamePatcherS {
    class PatchProcessor {
        internal Bitmap GameLogo { get; private set; }
        internal string GameDescription { get; private set; }
        internal string GameTitle { get; private set; }
        internal bool NeedOverwrite { get; private set; }
        internal int TotalFiles { get; private set; }

        internal delegate void IntDataDelegate(int data);
        internal delegate void StrDataDelegate(string data);
        internal delegate void EmptyDelegate();

        internal event IntDataDelegate UpdateProgress = delegate { };
        internal event EmptyDelegate Finished = delegate { };
        internal event StrDataDelegate FinishedError = delegate { };

        PatchContainer patchContainer;
        readonly string[] supportedExtentions = new string[] { ".xdf", ".dim", ".hdm" };
        string[] sourceImages;
        string sourceFolder;
        string destinationFolder;

        internal void LoadPatch(string path) {
            patchContainer = PatchContainer.Load(path);
            if (patchContainer == null) throw new Exception("Malformed patch file");
            Init();
        }

        private void Init() {
            GameDescription = patchContainer.Description;
            if (string.IsNullOrEmpty(patchContainer.Title)) {
                GameTitle = GameDescription;
            } else {
                GameTitle = patchContainer.Title;
            }
            if (patchContainer.LogoImage != null && patchContainer.LogoImage.Length > 0) {
                GameLogo = new Bitmap(new MemoryStream(patchContainer.LogoImage));
            } else {
                GameLogo = null;
            }
            TotalFiles = patchContainer.TotalSourceFiles();
        }

        internal void LoadPatch(byte[] patch) {
            patchContainer = PatchContainer.Load(patch);
            if (patchContainer == null) throw new Exception("Malformed patch data");
            Init();
        }

        internal void SetSourceFolder(string path) {
            var files = Directory.GetFiles(path).Where(fname => {
                var ext = Path.GetExtension(fname.ToLower());
                return supportedExtentions.Any(e => ext == e);
            }).ToArray();
            if (files.Length == 0) {
                throw new FileNotFoundException("Source folder should contains supported images (*.xdf, *.dim, *.hdm)");
            }
            sourceFolder = path;
            sourceImages = files;
        }

        internal void SetDestinationFolder(string path) {
            var files = Directory.GetFiles(path).Select(fname => Path.GetFileName(fname.ToLower()));
            var sfiles = sourceImages.Select(fname => Path.GetFileName(fname.ToLower()));
            NeedOverwrite = files.Any(fname => sfiles.Any(sfname => fname == sfname));
            destinationFolder = path;
        }

        internal void Patch() {
            new Thread(() => {
                Thread.CurrentThread.IsBackground = true;
                try {
                    Run();
                    writelog("Finished");
                    Finished();
                } catch (Exception ex) {
                    writelog(ex.Message);
                    writelog(ex.StackTrace);
                    ClearDestination();
                    FinishedError(ex.Message);
                }
            }).Start();
        }

        private void writelog(string msg) {
            Debug.WriteLine(msg);
            Console.WriteLine(msg);
        }

        private void Run() {
            CopySource();
            VirtualDisk src = null;
            VirtualDisk dst = null;
            PC98FatFileSystem srcFat = null;
            PC98FatFileSystem dstFat = null;
            int currentN = 0;
            int totalFiles = 0;
            string dstName = "";
            try {
                foreach (var file in patchContainer.PatchData) {
                    if (file.ImageNum != currentN) {
                        if (srcFat != null) srcFat.Dispose();
                        if (src != null) src.Dispose();
                        if (dstFat != null) dstFat.Dispose();
                        if (dst != null) dst.Dispose();
                        if (!string.IsNullOrEmpty(dstName)) CheckConsistency(currentN, dstName);
                        currentN = file.ImageNum;
                        FindImageByNum(currentN, out dstName, out src, out dst, out srcFat, out dstFat);
                    }
                    var data = CheckSourceFile(srcFat, file.Name, file.OriginalMd5Sum);
                    if (file.Action == PatchAction.Original) {
                        writelog($"[KEEP] {file.Name}");
                    } else {
                        writelog($"[PATCH] {file.Name}");
                        ApplyPatch(dstFat, file.Name, data, file.Patch);
                    }
                    totalFiles++;
                    UpdateProgress(totalFiles);
                }
            } finally {
                if (srcFat != null) srcFat.Dispose();
                if (src != null) src.Dispose();
                if (dstFat != null) dstFat.Dispose();
                if (dst != null) dst.Dispose();
                if (!string.IsNullOrEmpty(dstName)) CheckConsistency(currentN, dstName);
            }
        }

        private void CheckConsistency(int currentN, string dstName) {
            using (var dst = VirtualDisk.OpenDisk(dstName, FileAccess.ReadWrite)) {
                using (var dstFat = new PC98FatFileSystem(dst.Content, new FileSystemParameters {
                    SectorSize = dst.SectorSize,
                    FileNameEncoding = Encoding.GetEncoding("shift-jis")
                })) {
                    for (var i = 0; i < patchContainer.PatchData.Count; i++) {
                        var file = patchContainer.PatchData[i];
                        if (file.ImageNum != currentN) continue;
                        var sum = file.Action == PatchAction.Patch ? file.PatchedMd5Sum : file.OriginalMd5Sum;
                        using (var dstfh = dstFat.OpenFile(file.Name, FileMode.Open)) {
                            var md5 = Utils.Md5sum(dstfh);
                            if (sum != md5) {
                                throw new Exception($"Broken output file {file.Name} in image {dstName}");
                            }
                        }
                    }
                }
            }
        }

        private void ApplyPatch(PC98FatFileSystem dstFat, string name, byte[] srcdata, byte[] patch) {
            dstFat.DeleteFile(name);
            using (var dstfh = dstFat.OpenFile(name, FileMode.Create)) {
                BsPatch.Apply(srcdata, patch, dstfh);
            }
        }

        private byte[] CheckSourceFile(PC98FatFileSystem srcFat, string name, string originalMd5Sum) {
            if (!srcFat.FileExists(name)) {
                throw new FileNotFoundException($"Can't find file {name} in source image");
            }
            using (var srcfh = srcFat.OpenFile(name, FileMode.Open)) {
                if (Utils.Md5sum(srcfh) != originalMd5Sum) {
                    throw new InvalidDataException($"Source file {name} content differs");
                }
                return Utils.DumpStream(srcfh);
            }
        }

        private void FindImageByNum(int currentN, out string dstName, out VirtualDisk src, out VirtualDisk dst, out PC98FatFileSystem srcFat, out PC98FatFileSystem dstFat) {
            string srcName = "";
            var patchFiles = PatchFiles(currentN);
            if (patchFiles.Length == 0) throw new FileNotFoundException($"Image with number {currentN} is empty");
            foreach (var fname in sourceImages) {
                var disk = VirtualDisk.OpenDisk(fname, FileAccess.Read);
                var fat = new PC98FatFileSystem(disk.Content, new FileSystemParameters {
                    SectorSize = disk.SectorSize,
                    FileNameEncoding = Encoding.GetEncoding("shift-jis")
                });
                var files = fat.GetFiles("\\").OrderBy(f => f).ToDictionary(f => f.ToLower(), f => true);
                var found = patchFiles.All(f => files.ContainsKey(f));
                if (found) {
                    srcName = fname;
                    break;
                } 
                fat.Dispose();
                disk.Dispose();
            }
            if (string.IsNullOrEmpty(srcName)) throw new FileNotFoundException($"Can't find source image file with number {currentN}");
            src = VirtualDisk.OpenDisk(srcName, FileAccess.Read);
            dstName = Path.Combine(destinationFolder, Path.GetFileName(srcName));
            dst = VirtualDisk.OpenDisk(dstName, FileAccess.ReadWrite);
            srcFat = new PC98FatFileSystem(src.Content, new FileSystemParameters {
                SectorSize = src.SectorSize,
                FileNameEncoding = Encoding.GetEncoding("shift-jis")
            });
            dstFat = new PC98FatFileSystem(dst.Content, new FileSystemParameters {
                SectorSize = dst.SectorSize,
                FileNameEncoding = Encoding.GetEncoding("shift-jis")
            });
        }

        private string[] PatchFiles(int currentN) {
            return patchContainer.PatchData.Where(f => {
                return f.ImageNum == currentN;
            }).Select(f => f.Name.ToLower()).OrderBy(f => f).ToArray();
        }

        private void CopySource() {
            foreach (var fname  in sourceImages) {
                writelog($"Copy source {fname}");
                var dst = Path.Combine(destinationFolder, Path.GetFileName(fname));
                if (File.Exists(dst)) {
                    writelog($"Unlink existing destination {dst}");
                    File.Delete(dst);
                }
                File.Copy(fname, dst);
            }
        }

        private void ClearDestination() {
            foreach (var fname in sourceImages) {
                var dst = Path.Combine(destinationFolder, Path.GetFileName(fname));
                if (File.Exists(dst)) {
                    writelog($"Unlink destination {dst}");
                    File.Delete(dst);
                }
            }
        }
    }
}
