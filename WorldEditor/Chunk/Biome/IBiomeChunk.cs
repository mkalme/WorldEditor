using System;

namespace WorldEditor {
    public interface IBiomeChunk {
        int[] Biomes { get; }

        void SetBiome(int biome, int x, int y, int z);
        int GetBiome(int x, int y, int z);
    }
}
