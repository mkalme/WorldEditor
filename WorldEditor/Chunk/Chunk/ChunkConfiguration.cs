using System;

namespace WorldEditor {
    public class ChunkConfiguration {
        public int MinY { get; set; }
        public int MaxY { get; set; }

        public ChunkConfiguration(int minY, int maxY) {
            MinY = minY;
            MaxY = maxY;
        }
    }
}
