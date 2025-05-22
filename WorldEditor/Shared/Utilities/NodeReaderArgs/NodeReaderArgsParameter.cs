using System;
using NbtEditor;

namespace WorldEditor {
    public class ChunkReaderArgs<TParameter> : ChunkReaderArgs {
        public TParameter Parameter { get; set; }

        public ChunkReaderArgs(Chunk chunk, CompoundTag nbtData, CompoundTag nbtLevel, ChunkLoadSettings settings) : base(chunk, nbtData, nbtLevel, settings) {

        }
    }
}
