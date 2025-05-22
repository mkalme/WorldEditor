using System;
using System.IO;

namespace WorldEditor {
    public class RegionWriter : IRegionWriter<Stream> {
        public CompressionType CompressionType { get; set; } = CompressionType.ZLib;
        public IChunkCompression ChunkCompression { get; set; }

        public IChunkWriter<Container<byte[]>> ChunkWriter { get; set; }

        public RegionWriter() {
            ChunkCompression = new ChunkCompression();
            ChunkWriter = new ChunkWriter();
        }

        public void Write(Region region, Stream stream) {
            var chunks = region.Chunks;

            using (var rsw = new RegionStreamWriter(stream)) {
                byte[][] chunkBytes = new byte[chunks.Count][];

                for (int i = 0; i < chunks.Count; i++) {
                    var container = new Container<byte[]>();
                    ChunkWriter.Write(chunks[i], container);

                    chunkBytes[i] = ChunkCompression.Compress(container, CompressionType);
                }

                rsw.Write(new byte[8192]);
                int chunkIndex = 8192;
                for (int i = 0; i < chunks.Count; i++) {
                    var chunk = chunks[i];
                    int headerOffset = GetLocationIndex(chunk.X, chunk.Z) * 4;

                    rsw.BaseStream.Position = headerOffset;
                    rsw.WriteInt24(chunkIndex / 4096);
                    rsw.Write((byte)Math.Ceiling(chunkBytes[i].Length / 4096.0));

                    rsw.BaseStream.Position += 4092;
                    rsw.Write(chunk.LastUpdate);

                    rsw.BaseStream.Position = chunkIndex;
                    rsw.Write(chunkBytes[i].Length);
                    rsw.Write((byte)CompressionType);
                    rsw.Write(chunkBytes[i]);

                    int padding = 4096 - ((int)rsw.BaseStream.Length % 4096);
                    rsw.Write(new byte[padding]);

                    chunkIndex += chunkBytes[i].Length + 5 + padding;
                }
            }
        }

        private static int GetLocationIndex(int x, int z) {
            return x.Mod(32) + (z.Mod(32) * 32);
        }
    }
}
