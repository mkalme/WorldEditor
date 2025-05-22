using System;
using NbtEditor;

namespace WorldEditor {
    public class ChunkReaderFactory : IChunkReaderFactory<(CompoundTag, ChunkLoadSettings)> {
        public IChunkReader<(CompoundTag, ChunkLoadSettings)> CreateReader((CompoundTag, ChunkLoadSettings) parameter) {
            int dataVersion = 0;

            if (parameter.Item1.TryGetValue("DataVersion", out Tag dataVersionTag)) {
                dataVersion = dataVersionTag;
            }

            switch (dataVersion) {
                case int n when n < 1451:
                    return CreatePreFlattenedReader(parameter);
                default:
                    return CreateFlattenedReader(parameter);
            }
        }

        protected virtual IChunkReader<(CompoundTag, ChunkLoadSettings)> CreateFlattenedReader((CompoundTag, ChunkLoadSettings) parameter) {
            return new FlattenedChunkReader();
        }
        protected virtual IChunkReader<(CompoundTag, ChunkLoadSettings)> CreatePreFlattenedReader((CompoundTag, ChunkLoadSettings) parameter) {
            return new PreFlattenedChunkReader();
        }
    }
}
