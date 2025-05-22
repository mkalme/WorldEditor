using System;

namespace WorldEditor {
    public class NewestBlockStateLocker : IBlockStateLocker {
        public long[] Lock(ushort[] blocks, Palette palette) {
            int length = palette.GetMinBitLength();

            int bitAmount = 64 / length * length;
            int iterations = bitAmount / length;

            long[] blockStates = new long[(int)Math.Ceiling(4096.0 / iterations)];

            int index = 0;
            for (int i = 0; i < blockStates.Length; i++) {
                long l = 0;

                for (int j = 0; j < bitAmount; j += length) {
                    if (index >= 4096) {
                        blockStates[i] = l;
                        return blockStates;
                    }

                    l = (long)((ulong)l | ((ulong)blocks[index] << j));

                    index++;
                }

                blockStates[i] = l;
            }

            return blockStates;
        }
    }
}
