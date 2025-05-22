using System;

namespace WorldEditor {
    public class NewestBlockStateUnlocker : IBlockStateUnlocker {
        public ushort[] Unlock(long[] blockStates, Palette palette) {
            ushort[] blocks = new ushort[4096];

            int length = palette.GetMinBitLength();

            int bitAmount = 64 / length * length;
            int iterations = bitAmount / length;
            int shiftLeft = 64 - length;

            int index = 0;
            for (int i = 0; i < blockStates.Length; i++) {
                long l = blockStates[i];
                if (l == 0) {
                    index += iterations;
                    continue;
                }

                for (int j = 0; j < bitAmount; j += length) {
                    if (index >= 4096) return blocks;

                    blocks[index] = (ushort)(short)l.ExtractBits(j, shiftLeft);

                    index++;
                }
            }

            return blocks;
        }
    }
}
