using System;

namespace WorldEditor {
    public class RegionChunkInput {
        public byte[] Bytes { get; set; }
        public int X { get; set; }
        public int Z { get; set; }
        public ChunkLoadSettings Settings { get; set; }

        public RegionChunkInput(byte[] bytes, int x, int z, ChunkLoadSettings settings) {
            Bytes = bytes;
            X = x;
            Z = z;
            Settings = settings;
        }
    }
}
