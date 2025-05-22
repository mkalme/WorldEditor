using System;

namespace WorldEditor {
    public class NewestPaletteUnlocker : IPaletteUnlocker {
        public IBlock[] UnlockPalette(long[] blockStates, Palette palette) {
            IBlock[] output = new IBlock[4096];

            int length = palette.GetMinBitLength();

            int bitAmount = 64 / length * length;
            int shiftLeft = 64 - length;

            int index = 0;
            for (int i = 0; i < blockStates.Length; i++) {
                long l = blockStates[i];

                for (int j = 0; j < bitAmount; j += length) {
                    if (index >= 4096) return output;

                    output[index] = palette.Blocks[l.ExtractBits(j, shiftLeft)];

                    index++;
                }
            }

            return output;
        }
    }
}
