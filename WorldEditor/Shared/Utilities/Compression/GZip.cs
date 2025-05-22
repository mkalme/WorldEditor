using System;
using System.IO;
using System.IO.Compression;

namespace WorldEditor {
    public static class GZip {
        public static MemoryStream Compress(byte[] input) {
            var mso = new MemoryStream();
            using (var msi = new MemoryStream(input))
            using (var gs = new GZipStream(mso, CompressionMode.Compress, true)) {
                msi.CopyTo(gs);

                return mso;
            }
        }
        public static MemoryStream Decompress(byte[] input, int index, int count) {
            var mso = new MemoryStream();
            using (var msi = new MemoryStream(input, index, count))
            using (var gs = new GZipStream(msi, CompressionMode.Decompress, true)) {
                gs.CopyTo(mso);

                return mso;
            }
        }
    }
}
