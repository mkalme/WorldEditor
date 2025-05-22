using System;
using NbtEditor;

namespace WorldEditor {
    public class RegionChunkReader : IChunkReader<RegionChunkInput> {
        public IChunkDecompression ChunkDecompression { get; set; }
        public IChunkReaderFactory<(CompoundTag, ChunkLoadSettings)> ChunkReaderFactory { get; set; }

        public RegionChunkReader() {
            ChunkDecompression = new ChunkDecompression();
            ChunkReaderFactory = new ChunkReaderFactory();
        }

        public bool TryRead(RegionChunkInput input, out Chunk chunk) {
            int index = (input.X.Mod(32) + input.Z.Mod(32) * 32) * 4;

            int location = MathHelper.ToInt24(input.Bytes, index) * 4096;
            int sectorSize = input.Bytes[index + 1] * 4096;

            chunk = null;
            if (location == 0 || sectorSize == 0) return false;

            int sizeInBytes = MathHelper.ToInt32(input.Bytes, location);
            CompressionType type = (CompressionType)input.Bytes[location + 4];

            byte[] chunkBytes = ChunkDecompression.Decompress(type, input.Bytes, location + 5, sizeInBytes);
            if (chunkBytes == null) return false;

            CompoundTag nbtData = Tag.FromBytes(chunkBytes) as CompoundTag;
            var reader = ChunkReaderFactory.CreateReader((nbtData, input.Settings));
            if (!reader.TryRead((nbtData, input.Settings), out chunk)) return false;
            chunk.LastUpdate = MathHelper.ToInt32(input.Bytes, index + 4096);

            return true;
        }
    }
}
