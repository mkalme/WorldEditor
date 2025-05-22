using System;
using Nibble4;

namespace WorldEditor {
    static class IdHelper {
        public static ushort CreateKey(ushort id, byte data) {
            return (ushort)(id | (data << 12));
        }
        public static byte ReadNibble(Nibble4Array nibbleArray, int index) {
            Nibble4.Nibble4 nibble = nibbleArray.ReadBothNibbles(index / 2);

            if (index % 2 == 0) return nibble.Low;
            return nibble.High;
        }
    }
}
