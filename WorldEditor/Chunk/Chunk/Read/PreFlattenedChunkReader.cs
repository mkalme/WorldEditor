using System;
using NbtEditor;

namespace WorldEditor {
    public class PreFlattenedChunkReader : ChunkReaderBase {
        public ISectionReaderFactory<ChunkReaderArgs<SectionReaderArgs>> SectionReaderFactory { get; set; }
        public IHeightmapReader<(Chunk, ArrayTag)> HeightmapReader { get; set; }

        public BiomeToFlattenedConverter BiomeConverter { get; set; }

        public IBlockStateLockerFactory BlockStateLockerFactory { get; set; }
        public IPaletteUnlockerFactory PaletteUnlockerFactory { get; set; }

        public PreFlattenedChunkReader() {
            SectionReaderFactory = new SectionReaderFactory();
            HeightmapReader = new PreFlattenedHeightmapReader();

            BiomeConverter = new BiomeToFlattenedConverter();

            BlockStateLockerFactory = new BlockStateLockerFactory();
            PaletteUnlockerFactory = new PaletteUnlockerFactory();
        }

        protected override void LoadBiomes(Chunk chunk, CompoundTag level) {
            base.LoadBiomes(chunk, level);
            BiomeConverter.Convert(chunk.Biomes.Biomes);
        }

        protected override void LoadSections(Chunk chunk, CompoundTag nbtData, CompoundTag level, ChunkLoadSettings settings) {
            if (!level.TryGetValue("Sections", out Tag listTag)) return;
            ListTag sections = listTag as ListTag;

            var args = new ChunkReaderArgs<SectionReaderArgs>(chunk, nbtData, level, settings) {
                Parameter = new SectionReaderArgs() {
                    Settings = settings.Section,
                    BlockStateLocker = BlockStateLockerFactory.CreateLocker(chunk),
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

        }
    }
}
