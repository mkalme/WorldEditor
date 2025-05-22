using System;
using NbtEditor;

namespace WorldEditor {
    static class TagExtension {
        public static byte[] ToByteArray(this Tag tag) {
            return (byte[])(Array)(sbyte[])tag;
        }
    }
}
