using System;
using System.Collections.Generic;

namespace WorldEditor {
    public class Chunk {
        public int X { get; set; } = 0;
        public int Z { get; set; } = 0;
        public Cords Cords { get => new Cords(X, Z); }

        public string Status { get; set; } = "empty";
        public int LastUpdate { get; set; } = 0;
        public long LastUpdateTick { get; set; } = 0;
        public int DataVersion { get; set; }

        public IDictionary<string, ushort[]> Heightmaps { get; set; }
        public IList<Section> Sections { get; set; }
        public IBiomeChunk Biomes { get; set; }
        public ChunkConfiguration Configuration { get; set; }

        public Chunk() {
            Sections = new List<Section>();
            Heightmaps = new Dictionary<string, ushort[]>();
        }
    }
}