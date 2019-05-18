using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SharedTypes {
    [Serializable]
    class PatchContainer {
        public static decimal DefaultPatchContainerVersion = 1.04M;
        public decimal PatchContainerVersion;
        public List<PatchedFile> PatchData;
        public long TotalSize = 0;
        public string Title;
        public string Description;
        public byte[] LogoImage;
        public string Platform;

        private int totalSourceFiles;
        public int FoundFiles = 0;

        public int TotalSourceFiles() {
            if (totalSourceFiles == 0) {
                foreach (var file in PatchData) {
                    if (file.Action == PatchAction.Original || file.Action == PatchAction.Patch) {
                        totalSourceFiles++;
                    }
                }
            }
            return totalSourceFiles;
        }

        public PatchContainer() {
            PatchContainerVersion = DefaultPatchContainerVersion;
            PatchData = new List<PatchedFile>();
        }

        public void Add(PatchedFile file) {
            this.PatchData.Add(file);
        }

        public void Save(string fileName) {
            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }
            using (var fs = File.OpenWrite(fileName)) {
                var serializer = new BinaryFormatter {
                    AssemblyFormat =
                    System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                };
                serializer.Serialize(fs, this);
            }
        }

        public byte[] Serialize() {
            using (var ms = new MemoryStream()) {
                var serializer = new BinaryFormatter {
                    AssemblyFormat =
                    System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                };
                serializer.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static PatchContainer Load(string filename) {
            var serializer = new BinaryFormatter {
                AssemblyFormat =
                System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                Binder = new DeserializationBinder()
            };
            using (var fs = File.OpenRead(filename)) {
                var data = serializer.Deserialize(fs);
                return (PatchContainer)data;
            }
        }

        public static PatchContainer Load(byte[] patch) {
            var serializer = new BinaryFormatter {
                AssemblyFormat =
                System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                Binder = new DeserializationBinder()
            };
            using (var ms = new MemoryStream(patch)) {
                var data = serializer.Deserialize(ms);
                return (PatchContainer)data;
            }
        }

        public string Stat() {
            var totalFiles = 0;
            var patchedFiles = 0;
            var patchLength = 0.0;
            foreach (var file in PatchData) {
                totalFiles++;
                if (file.Action == PatchAction.Patch) {
                    patchedFiles++;
                    patchLength += file.Patch.Length;
                }
            }
            return
                $"Total files: {totalFiles}\r\nPatchedFiles: {patchedFiles}\r\nPatch length: {(patchLength / 1024):F2} Kb";
        }

        public void ClearState() {
            foreach (var file in PatchData) {
                file.Found = false;
                file.Processed = false;
                FoundFiles = 0;
            }
        }

        public string[] ShowNotFoundFiles() {
            var files = new List<string>();
            foreach (var file in PatchData) {
                if ((file.Action == PatchAction.Original || file.Action == PatchAction.Patch) && !file.Found) {
                    files.Add(file.Name);
                }
            }
            return files.ToArray();
        }
    }
}
