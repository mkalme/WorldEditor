using System;

namespace WorldEditor {
    static class BlockStateHelper {
        public static ushort[] Unlock(long[] array, int length) {
            ushort[] blocks = new ushort[array.Length * 64 / length];

            ushort carry = 0;
            int carryLength = 0;

            int index = 0;
            for (int i = 0; i < array.Length; i++) {
                long l = array[i];

                if (carryLength > 0) {
                    blocks[index++] = (ushort)(carry | (ushort)(l.ExtractBits(0, 64 - carryLength) << (length - carryLength)));
                }

                int n = carryLength;
                while (n + length <= 64) {
                    blocks[index++] = (ushort)(short)l.ExtractBits(n, 64 - length);
                    n += length;
                }

                int leftBits = 64 - n;
                if (leftBits > 0) {
                    carry = (ushort)(short)l.ExtractBits(n, n);
                    carryLength = length - leftBits;
                } else {
                    carry = 0;
                    carryLength = 0;
                }
            }

            return blocks;
        }
    }
}
