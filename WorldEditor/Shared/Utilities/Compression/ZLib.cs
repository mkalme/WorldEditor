using System;
using System.IO;
using ZLibNet;

namespace WorldEditor {
    public static class ZLib {
        public static MemoryStream Compress(byte[] input) {
            var mso = new MemoryStream();
            using (var msi = new MemoryStream(input))
            using (var zs = new ZLibStream(mso, CompressionMode.Compress, CompressionLevel.Level6, true)) {
                msi.CopyTo(zs);

                return mso;
            }
        }
        public static MemoryStream Decompress(byte[] input, int index, int count) {
            var mso = new MemoryStream();
            using (var msi = new MemoryStream(input, index, count))
            using (var zs = new ZLibStream(msi, CompressionMode.Decompress, true)) {
                zs.CopyTo(mso);

                return mso;
            }
        }
    }
}
