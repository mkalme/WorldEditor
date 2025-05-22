using System;

namespace WorldEditor {
    public struct Cords {
        public int X { get; set; }
        public int Z { get; set; }

        public Cords(int x, int z) {
            X = x;
            Z = z;
        }
    }
}
