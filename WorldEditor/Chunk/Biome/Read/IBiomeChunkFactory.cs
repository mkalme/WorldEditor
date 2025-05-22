using System;
using NbtEditor;

namespace WorldEditor {
    public interface IBiomeChunkFactory {
        IBiomeChunk CreateBiomeChunk(Chunk chunk, Tag biomes);
    }
}
