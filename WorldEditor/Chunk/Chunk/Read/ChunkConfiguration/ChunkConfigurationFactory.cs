using System;

namespace WorldEditor {
    public class ChunkConfigurationFactory : IChunkConfigurationFactory {
        public virtual ChunkConfiguration CreateConfiguration(Chunk chunk) {
            if (chunk.DataVersion < 2694) return new ChunkConfiguration(0, 256);
            
            return CreateConfigurationFromChunk(chunk);
        }

        protected virtual ChunkConfiguration CreateConfigurationFromChunk(Chunk chunk) {
            if (chunk.Sections.Count == 0 || chunk.Biomes == null) return new ChunkConfiguration(-64, 320);

            int minY = (chunk.Sections[0].Y + 1) * 16;
            int maxY = minY + chunk.Biomes.Biomes.Length / 4;

            return new ChunkConfiguration(minY, maxY);
        }
    }
}
