using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace WorldEditor {
    static class ZLibOutputHelper {
        public static MemoryStream Compress(byte[] input) {
            var mso = new MemoryStream();

            using (var msi = new MemoryStream(input))
            using (var deflator = new DeflaterOutputStream(mso)) {
                msi.CopyTo(deflator);
            }

            return mso;
        }
    }
}