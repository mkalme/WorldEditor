using System;

namespace WorldEditor {
    public class BiomeChunk3D : IBiomeChunk {
        public int[] Biomes { get; set; }

        public BiomeChunk3D(int[] biomes) {
            Biomes = biomes;
        }
        public BiomeChunk3D(sbyte[] biomes) {
            Biomes = new int[biomes.Length];
            for (int i = 0; i < biomes.Length; i++) {
                Biomes[i] = biomes[i] + 128;
            }
        }

        public virtual void SetBiome(int biome, int x, int y, int z) {
            Biomes[x / 4 + y / 4 * 16 + z / 4 * 4] = biome;
        }
        public virtual int GetBiome(int x, int y, int z) {
            return Biomes[x / 4 + y / 4 * 16 + z / 4 * 4];
        }
    }
}
