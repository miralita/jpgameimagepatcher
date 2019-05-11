using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes {
    enum PatchAction {
        Delete, Copy, Ignore, Ask, Patch, Original
    }
    [Serializable]
    class PatchedFile {
        public PatchAction Action;
        public int ImageNum;
        public string Name;
        public byte[] Patch;
        public string OriginalMd5Sum;
        public string PatchedMd5Sum;
        public bool Processed;
        public bool Found;

        public override string ToString() {
            return Name;
        }
    }
}
