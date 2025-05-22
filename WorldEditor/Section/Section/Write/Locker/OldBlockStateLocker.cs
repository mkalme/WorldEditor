using System;

namespace WorldEditor {
    public class OldBlockStateLocker : IBlockStateLocker {
        public long[] Lock(ushort[] blocks, Palette palette) {
            int length = palette.GetMinBitLength();

            long[] blockStates = new long[length * 64];

            long carry = 0;
            int carryLength = 0;

            int index = 0;
            for (int i = 0; i < blockStates.Length; i++) {
                long l = 0;

                if (carryLength > 0) {
                    l = carry;
                    index++;
                }

                int n = carryLength;
                while (n + length <= 64) {
                    l = (long)((ulong)l | ((ulong)blocks[index++] << n));
                    n += length;
                }

                int leftBits = 64 - n;
                if (leftBits > 0) {
                    l = (long)((ulong)l | ((ulong)blocks[index] << (n)));

                    carry = (long)((ulong)blocks[index] >> leftBits);
                    carryLength = length - leftBits;
                } else {
                    carry = 0;
                    carryLength = 0;
                }

                blockStates[i] = l;
            }

            return blockStates;
        }
    }
}
