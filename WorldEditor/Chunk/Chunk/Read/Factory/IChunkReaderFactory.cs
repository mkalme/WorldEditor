using System;
using NbtEditor;

namespace WorldEditor {
    public interface IChunkReaderFactory<TChunkParameter> {
        IChunkReader<TChunkParameter> CreateReader((CompoundTag, ChunkLoadSettings) parameter);
    }
}
