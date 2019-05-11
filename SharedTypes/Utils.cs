using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharedTypes {
    public static class Utils {
        public static string Md5sum(Stream file) {
            using (var md5 = MD5.Create()) {
                return BitConverter.ToString(md5.ComputeHash(file)).Replace("-", "");
            }
        }

        public static byte[] DumpStream(Stream s) {
            s.Seek(0, SeekOrigin.Begin);
            using (var ms = new MemoryStream()) {
                s.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
