using System;

namespace WorldEditor {
    public class ChunkCompression : IChunkCompression {
        public byte[] Compress(byte[] data, CompressionType compressionType) {
            switch (compressionType) {
                case CompressionType.GZip:
                    return GZip.Compress(data).ToArray();
                case CompressionType.ZLib:
                    return ZLibOutputHelper.Compress(data).ToArray();
                case CompressionType.Uncompressed:
                    return data;
            }

            return null;
        }
    }
}
