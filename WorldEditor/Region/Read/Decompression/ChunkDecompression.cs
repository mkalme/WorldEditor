using System;

namespace WorldEditor {
    public class ChunkDecompression : IChunkDecompression {
        public byte[] Decompress(CompressionType compressionType, byte[] input, int location, int size) {
            if (location + size >= input.Length) return null;

            switch (compressionType) {
                case CompressionType.ZLib:
                    return ZLib.Decompress(input, location, size).GetBuffer();
                case CompressionType.GZip:
                    return GZip.Decompress(input, location, size).GetBuffer();
                case CompressionType.Uncompressed:
                    byte[] bytes = new byte[size];
                    Buffer.BlockCopy(input, location, bytes, 0, size);
                    return bytes;
                default:
                    return null;
            }
        }
    }
}
