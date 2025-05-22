using System;
using NbtEditor;

namespace WorldEditor {
    public class BiomeReader : IBiomeReader<(Chunk, CompoundTag)> {
        public IBiomeChunkFactory BiomeChunkFactory { get; set; }

        public BiomeReader() {
            BiomeChunkFactory = new BiomeChunkFactory();
        }

        public bool TryRead((Chunk, CompoundTag) parameter, out IBiomeChunk biome) {
            Chunk chunk = parameter.Item1;
            CompoundTag level = parameter.Item2;

            if (!level.TryGetValue("Biomes", out Tag biomes)) {
                biome = null;
                return false;
            }

            biome = BiomeChunkFactory.CreateBiomeChunk(chunk, biomes);
            return biome != null;
        }
    }
}
