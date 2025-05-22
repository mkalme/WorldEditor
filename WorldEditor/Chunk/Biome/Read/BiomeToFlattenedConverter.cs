using System;

namespace WorldEditor {
    public class BiomeToFlattenedConverter {
        public virtual void Convert(int[] biomes) {
            for (int i = 0; i < biomes.Length; i++) {
                if (biomes[i] > 127) biomes[i] -= 128;
            }
        }
    }
}
