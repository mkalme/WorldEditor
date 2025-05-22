using System;

namespace Nibble4 {
    public struct Nibble4 : IEquatable<Nibble4>, ICloneable {
        public byte High { get; set; }
        public byte Low { get; set; }

        public byte Byte => (byte)(High * 16 + Low);

        public static implicit operator Nibble4(byte value) {
            return new Nibble4(value);
        }
        public static implicit operator Nibble4((byte, byte) tuple) {
            return new Nibble4(tuple.Item1, tuple.Item2);
        }

        public static implicit operator byte(Nibble4 nibble) {
            return (byte)(nibble.High * 16 + nibble.Low);
        }
        public static implicit operator (byte, byte)(Nibble4 nibble) {
            return (nibble.High, nibble.Low);
        }

        public Nibble4(byte high, byte low) {
            High = high;
            Low = low;
        }
        public Nibble4(byte value) {
            High = (byte)(value / 16);
            Low = (byte)(value % 16);
        }

        //Default Methods
        public override string ToString() {
            return Byte.ToString();
        }
        public object Clone() {
            return new Nibble4(High, Low);
        }
        public bool Equals(Nibble4 other) {
            return High == other.High && Low == other.Low;
        }
    }
}