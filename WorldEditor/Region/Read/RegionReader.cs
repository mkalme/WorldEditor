using System;
using System.IO;
using NbtEditor;

namespace WorldEditor {
    public class RegionReader : IRegionReader<(string, ChunkLoadSettings)> {
        public IChunkDecompression ChunkDecompression { get; set; }
        public IChunkReaderFactory<(CompoundTag, ChunkLoadSettings)> ChunkReaderFactory { get; set; }

        public RegionReader() {
            ChunkDecompression = new ChunkDecompression();
            ChunkReaderFactory = new ChunkReaderFactory();
        }

        public bool TryRead((string, ChunkLoadSettings) parameter, out Region region) {
            string filePath = parameter.Item1;
            ChunkLoadSettings settings = parameter.Item2;

            region = new Region(Region.GetCoordinates(filePath));

            byte[] bytes = File.ReadAllBytes(filePath);

            if (bytes.Length == 0) return false;
            byte[][] chunkBytes = new byte[1024][];

            for (int i = 0; i < 1024; i++) {
                int location = MathHelper.ToInt24(bytes, i * 4) * 4096;
                int sectorSize = bytes[i * 4 + 3] * 4096;

                if (location == 0 || sectorSize == 0) continue;

                int sizeInBytes = MathHelper.ToInt32(bytes, location);
                CompressionType type = (CompressionType)bytes[location + 4];

                chunkBytes[i] = ChunkDecompression.Decompress(type, bytes, location + 5, sizeInBytes);
            }

            for (int i = 0; i < 1024; i++) {
                if (chunkBytes[i] == null) continue;

                CompoundTag nbtData = Tag.FromBytes(chunkBytes[i]) as CompoundTag;
                var reader = ChunkReaderFactory.CreateReader((nbtData, settings));
                if (reader.TryRead((nbtData, settings), out Chunk chunk)) {
                    chunk.LastUpdate = MathHelper.ToInt32(bytes, i * 4 + 4096);

                    region.Chunks.Add(chunk);
                }
            }

            return true;
        }
    }
}
