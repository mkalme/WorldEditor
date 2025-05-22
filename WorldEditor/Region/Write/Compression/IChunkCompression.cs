using System;

namespace WorldEditor {
    public interface IChunkCompression {
        byte[] Compress(byte[] data, CompressionType compressionType);
    }
}
