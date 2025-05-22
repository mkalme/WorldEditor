using System;
using System.Collections.Generic;
using NbtEditor;

namespace WorldEditor {
    public class FlattenedChunkReader : ChunkReaderBase {
        public ISectionReaderFactory<ChunkReaderArgs<SectionReaderArgs>> SectionReaderFactory { get; set; }
        public IHeightmapReader<(Chunk, ArrayTag)> HeightmapReader { get; set; }

        public IBlockStateLockerFactory BlockStateLockerFactory { get; set; }
        public IBlockStateUnlockerFactory BlockStateUnlockerFactory { get; set; }
        public IPaletteUnlockerFactory PaletteUnlockerFactory { get; set; }

        public FlattenedChunkReader() {
            SectionReaderFactory = new SectionReaderFactory();
            HeightmapReader = new HeightmapReader();

            BlockStateLockerFactory = new BlockStateLockerFactory();
            BlockStateUnlockerFactory = new BlockStateUnlockerFactory();
            PaletteUnlockerFactory = new PaletteUnlockerFactory();
        }

        protected override void LoadSections(Chunk chunk, CompoundTag nbtData, CompoundTag level, ChunkLoadSettings settings) {
            if (!level.TryGetValue("Sections", out Tag listTag)) return;
            ListTag sections = listTag as ListTag;

            var args = new ChunkReaderArgs<SectionReaderArgs>(chunk, nbtData, level, settings) {
                Parameter = new SectionReaderArgs() {
                    Settings = settings.Section,
                    BlockStateLocker = BlockStateLockerFactory.CreateLocker(chunk),
                    BlockStateUnlocker = BlockStateUnlockerFactory.CreateUnlocker(chunk),
                    PaletteUnlocker = PaletteUnlockerFactory.CreateUnlocker(chunk)
                }
            };

            int count = sections.Count;
            for (int i = 0; i < count; i++) {
                args.Parameter.NbtData = sections[i] as CompoundTag;

                var reader = SectionReaderFactory.CreateReader(args);
                if (!reader.TryRead(args, out Section section)) continue;

                chunk.Sections.Add(section);
            }
        }
        protected override void LoadHeightmaps(Chunk chunk, CompoundTag level, HeightmapLoadSettings settings) {
            if (!level.TryGetValue("Heightmaps", out Tag heightmapTag)) return;

            CompoundTag heightmap = heightmapTag as CompoundTag;

            IList<string> heightmaps = settings.Types;
            for (int i = 0; i < heightmaps.Count; i++) {
                if (heightmap.TryGetValue(heightmaps[i], out Tag worldSurface)) {
                    if (!HeightmapReader.TryRead((chunk, worldSurface as ArrayTag), out ushort[] map)) continue;

                    chunk.Heightmaps.Add(heightmaps[i], map);
                }
            }
        }
    }
}
