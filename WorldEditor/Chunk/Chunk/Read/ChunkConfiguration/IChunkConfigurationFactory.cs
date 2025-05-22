using System;

namespace WorldEditor {
    public interface IChunkConfigurationFactory {
        ChunkConfiguration CreateConfiguration(Chunk chunk);
    }
}
