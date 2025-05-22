using System;
using System.IO;

namespace WorldEditor {
    public class RegionStreamWriter : BinaryWriter {
        public RegionStreamWriter(Stream output) : base(output) {

        }

        public override void Write(int value) {
            base.Write(new byte[] { (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });
        }
        public void WriteInt24(int value) {
            base.Write(new byte[] { (byte)(value >> 16), (byte)(value >> 8), (byte)value });
        }
    }
}
