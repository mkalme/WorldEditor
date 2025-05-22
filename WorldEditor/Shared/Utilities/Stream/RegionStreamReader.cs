using System;
using System.IO;

namespace WorldEditor {
    sealed class RegionStreamReader : Stream {
        private byte[] _buffer { get; set; }
        public override long Position { get; set; }
        public override long Length => _buffer.Length;

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;

        public RegionStreamReader(byte[] buffer) {
            _buffer = buffer;
        }

        public override void Flush() {
            _buffer = new byte[0];
            Position = 0;
        }
        public unsafe override int Read(byte[] buffer, int offset, int count) {
            if (buffer.Length - offset < count) throw new ArgumentOutOfRangeException();

            int len = (int)(_buffer.Length - Position);

            if (len <= 0) return 0;
            if (len > count) len = count;

            fixed (byte* b = &_buffer[Position]) {
                fixed (byte* o = buffer) {
                    Buffer.MemoryCopy(b, o + offset, len, len);
                }
            }

            Position += len;

            return len;
        }

        public new byte ReadByte() {
            return _buffer[Position++];
        }
        public int ReadInt24() {
            return (_buffer[Position++] << 16) | (_buffer[Position++] << 8) | _buffer[Position++];
        }
        public int ReadInt32() {
            return (_buffer[Position++] << 24) | (_buffer[Position++] << 16) | (_buffer[Position++] << 8) | _buffer[Position++];
        }

        public byte ReadByte(int index) {
            return _buffer[index];
        }
        public unsafe int ReadInt24(int index) {
            return (_buffer[index] << 16) | (_buffer[index + 1] << 8) | _buffer[index + 2];
        }
        public unsafe int ReadInt32(int index) {
            return (_buffer[index] << 24) | (_buffer[index + 1] << 16) | (_buffer[index + 2] << 8) | _buffer[index + 3];
        }

        public byte[] GetBuffer() => _buffer;

        //Not Implemented
        public override void SetLength(long value) {
            throw new NotImplementedException();
        }
        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException();
        }
        public override void Write(byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }
    }
}
