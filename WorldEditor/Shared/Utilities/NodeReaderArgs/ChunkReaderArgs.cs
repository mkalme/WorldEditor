using System;
using NbtEditor;

namespace WorldEditor {
    public class ChunkReaderArgs {
        public Chunk Chunk { get; set; }
        public CompoundTag NbtData { get; set; }
        public CompoundTag NbtLevel { get; set; }
        public ChunkLoadSettings Settings { get; set; }

        public ChunkReaderArgs(Chunk chunk, CompoundTag nbtData, CompoundTag nbtLevel, ChunkLoadSettings settings) {
            Chunk = chunk;
            NbtData = nbtData;
            NbtLevel = nbtLevel;
            Settings = settings;
        }
    }
}
