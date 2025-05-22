using System;

namespace WorldEditor {
    static class MathHelper {
        public static int ToInt32(byte[] bytes, int index) {
            return bytes[index + 3] | (bytes[index + 2] << 8) | (bytes[index + 1] << 16) | (bytes[index] << 24);
        }
        public static int ToInt24(byte[] bytes, int index) {
            return bytes[index + 2] | (bytes[index + 1] << 8) | (bytes[index] << 16);
        }

        public static ulong ExtractBits(this long l, int n, int shiftLeft) {
            return (ulong)((l >> n) << shiftLeft) >> shiftLeft;
        }
        public static int Mod(this int x, int m) {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
