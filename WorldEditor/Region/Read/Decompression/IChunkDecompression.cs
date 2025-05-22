using System;

namespace WorldEditor {
    public interface IChunkDecompression {
        byte[] Decompress(CompressionType compressionType, byte[] input, int location, int size);
    }
}
