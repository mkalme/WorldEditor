using System;

namespace WorldEditor {

    public class BiomeChunk2D : BiomeChunk3D {
        public BiomeChunk2D(int[] biomes) : base(biomes) {

        }
        public BiomeChunk2D(sbyte[] biomes) : base(biomes) {

        }

        public override void SetBiome(int biome, int x, int y, int z) {
            Biomes[x + z * 16] = biome;
        }
        public override int GetBiome(int x, int y, int z) {
            return Biomes[x + z * 16];
        }
    }
}
