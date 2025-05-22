using System;
using System.Collections;

namespace Nibble4 {
    public class Nibble4Array : IEquatable<Nibble4Array>, ICloneable, IEnumerable {
        public byte[] Bytes { get; set; }
        public int Length => Bytes.Length * 2;
        public int LengthInBytes => Bytes.Length;

        public byte this[int index] {
            get => ReadNibble(index);
            set => WriteNibble(value, index);
        }

        public static implicit operator Nibble4Array(byte[] bytes) {
            return new Nibble4Array(bytes);
        }
        public static implicit operator Nibble4Array(sbyte[] bytes) {
            return new Nibble4Array(bytes);
        }

        public Nibble4Array(int capacityInBytes) {
            Bytes = new byte[capacityInBytes];
        }
        public Nibble4Array(byte[] bytes) {
            Bytes = bytes;
        }
        public Nibble4Array(sbyte[] bytes) {
            Bytes = (byte[])(Array)bytes;
        }

        public byte ReadNibble(int index) {
            byte source = Bytes[index / 2];

            if (index % 2 == 0) return (byte)(source / 16);
            return (byte)(source % 16);
        }
        public Nibble4 ReadBothNibbles(int byteIndex) {
            byte source = Bytes[byteIndex];

            return new Nibble4((byte)(source / 16), (byte)(source % 16));
        }

        public void WriteNibble(byte value, int index) {
            int at = index / 2;

            if (index % 2 == 0) {
                byte lowNibble = (byte)(Bytes[at] & 0x0F);

                Bytes[at] = (byte)(value << 4 + lowNibble);
            } else {
                byte hiNibble = (byte)(Bytes[at] & 0xF0);

                Bytes[at] = (byte)(value + (hiNibble << 4));
            }
        }
        public void WriteBothNibbles(byte high, byte low, int byteIndex) {
            Bytes[byteIndex] = (byte)(high * 16 + low);
        }
        public void WriteBothNibbles(Nibble4 nibble, int byteIndex) {
            Bytes[byteIndex] = nibble.Byte;
        }

        //Default Methods
        public bool Equals(Nibble4Array other) {
            if (other == null) return false;
            if (Length != other.Length) return false;

            for (int i = 0; i < Bytes.Length; i++) {
                if (Bytes[i] != other.Bytes[i]) return false;
            }

            return true;
        }
        public object Clone() {
            return new Nibble4Array(Bytes);
        }
        public IEnumerator GetEnumerator() {
            return Bytes.GetEnumerator();
        }
    }
}
