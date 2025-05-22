using System;
using System.Collections.Generic;
using System.IO;

namespace WorldEditor {
    public class Region {
        public int X { get; set; } = 0;
        public int Z { get; set; } = 0;
        public Cords Cords { get => new Cords(X, Z); }

        public IList<Chunk> Chunks { get; set; }

        public IRegionWriter<Stream> RegionWriter { get; set; }

        public Region() {
            Chunks = new List<Chunk>(1024);

            RegionWriter = new RegionWriter();
        }
        public Region(Cords cords) : this() {
            X = cords.X;
            Z = cords.Z;
        }

        public static string GetFileName(int x, int y) {
            return $"r.{x}.{y}.mca";
        }
        public static Cords GetCoordinates(string fileName) {
            string[] split = Path.GetFileName(fileName).Split('.');

            return new Cords(int.Parse(split[1]), int.Parse(split[2]));
        }

        //Deserialize
        public static Region FromFile(string filePath) {
            return FromFile(filePath, ChunkLoadSettings.Default);
        }
        public static Region FromFile(string filePath, ChunkLoadSettings chunkSettings) {
            var regionReader = new RegionReader();

            regionReader.TryRead((filePath, chunkSettings), out Region region);

            return region;
        }
        public static Region FromRegionReader(string filePath, ChunkLoadSettings chunkSettings, IRegionReader<(string, ChunkLoadSettings)> regionReader) {
            regionReader.TryRead((filePath, chunkSettings), out Region region);

            return region;
        }

        public static Chunk ReadChunk(byte[] bytes, int x, int z, ChunkLoadSettings chunkSettings) {
            RegionChunkReader reader = new RegionChunkReader();

            reader.TryRead(new RegionChunkInput(bytes, x, z, chunkSettings), out Chunk chunk);

            return chunk;
        }

        //Serialize
        public void SaveToFile(string folderPath) {
            string fileName = GetFileName(X, Z);

            using (FileStream fileStream = new FileStream($"{folderPath}\\{fileName}", FileMode.OpenOrCreate)) {
                fileStream.SetLength(0);

                WriteToStream(fileStream);
            }
        }
        public void WriteToStream(Stream stream) {
            RegionWriter.Write(this, stream);
        }
    }
}
