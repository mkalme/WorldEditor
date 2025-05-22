using System;
using System.Collections.Generic;
using NbtEditor;

namespace WorldEditor {
    public abstract class ChunkReaderBase : IChunkReader<(CompoundTag, ChunkLoadSettings)> {
        public IBiomeReader<(Chunk, CompoundTag)> BiomeReader { get; set; }
        public IChunkConfigurationFactory ChunkConfigurationFactory { get; set; }

        public ChunkReaderBase() {
            BiomeReader = new BiomeReader();
            ChunkConfigurationFactory = new ChunkConfigurationFactory();
        }

        public bool TryRead((CompoundTag, ChunkLoadSettings) parameter, out Chunk chunk) {
            CompoundTag nbtData = parameter.Item1;
            ChunkLoadSettings settings = parameter.Item2;

            var level = nbtData["Level"] as CompoundTag;

            chunk = new Chunk() {
                X = level["xPos"],
                Z = level["zPos"],
                LastUpdateTick = level["LastUpdate"]
            };

            if (level.TryGetValue("Status", out Tag statusTag)) {
                chunk.Status = statusTag;
            } else {
                chunk.Status = "full";
            }

            if (nbtData.TryGetValue("DataVersion", out Tag dataVersionTag)) {
                chunk.DataVersion = dataVersionTag;
            } else {
                chunk.DataVersion = 0;
            }

            if (settings.LoadBiomes) {
                LoadBiomes(chunk, level);
            }

            if (settings.Section.LoadSections) {
                LoadSections(chunk, nbtData, level, settings);
            }

            if (settings.Heightmap.LoadHeightmap) {
                LoadHeightmaps(chunk, level, settings.Heightmap);
            }

            chunk.Configuration = ChunkConfigurationFactory.CreateConfiguration(chunk);

            return true;
        }

        protected virtual void LoadBiomes(Chunk chunk, CompoundTag level) {
            if (BiomeReader.TryRead((chunk, level), out IBiomeChunk biome)) {
                chunk.Biomes = biome;
            }
        }

        protected abstract void LoadSections(Chunk chunk, CompoundTag nbtData, CompoundTag level, ChunkLoadSettings settings);
        protected abstract void LoadHeightmaps(Chunk chunk, CompoundTag level, HeightmapLoadSettings settings);
    }
}
