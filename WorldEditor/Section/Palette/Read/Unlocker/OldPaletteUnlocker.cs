using System;

namespace WorldEditor {
    public class OldPaletteUnlocker : IPaletteUnlocker {
        public IBlock[] UnlockPalette(long[] blockStates, Palette palette) {
            IBlock[] output = new IBlock[4096];

            int length = palette.GetMinBitLength();

            int bitAmount = 64 / length * length;
            int shiftLeft = 64 - length;

            ushort carry = 0;
            int carryLength = 0;

            int index = 0;
            for (int i = 0; i < blockStates.Length; i++) {
                long l = blockStates[i];

                if (carryLength > 0) {
                    output[index] = palette.Blocks[(ushort)(carry | (ushort)(l.ExtractBits(0, 64 - carryLength) << (length - carryLength)))];
                    index++;
                }

                for (int j = carryLength; j < bitAmount + carryLength; j += length) {
                    output[index++] = palette.Blocks[(ushort)(short)l.ExtractBits(j, shiftLeft)];
                }

                int leftBits = 64 - bitAmount - carryLength;
                if (leftBits > 0) {
                    carry = (ushort)(short)l.ExtractBits(carryLength + bitAmount, carryLength + bitAmount);
                    carryLength = length - leftBits;
                } else {
                    carry = 0;
                    carryLength = 0;
                }
            }

            return output;
        }
    }
}
