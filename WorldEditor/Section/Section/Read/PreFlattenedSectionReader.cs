using System;
using NbtEditor;

namespace WorldEditor {
    public class PreFlattenedSectionReader : ISectionReader<ChunkReaderArgs<SectionReaderArgs>> {
        public PreFlattenedPaletteReader PaletteReader { get; set; }
        public ISectionFilter SectionFilter { get; set; }

        public PreFlattenedSectionReader() {
            PaletteReader = new PreFlattenedPaletteReader(ID.Translator);
            SectionFilter = new SectionFilter();
        }

        public bool TryRead(ChunkReaderArgs<SectionReaderArgs> parameter, out Section section) {
            section = new Section();

            if (parameter.Parameter.NbtData.TryGetValue("Y", out Tag y)) {
                section.Y = y;
            } else {
                return false;
            }

            section.BlockStateLocker = parameter.Parameter.BlockStateLocker;
            section.PaletteUnlocker = parameter.Parameter.PaletteUnlocker;

            LoadLight(section, parameter);
            LoadPalette(section, parameter);

            return SectionFilter.Filter(section, parameter.Parameter.Settings);
        }

        private void LoadLight(Section section, ChunkReaderArgs<SectionReaderArgs> parameter) {
            var nbtData = parameter.Parameter.NbtData;
            var settings = parameter.Parameter.Settings;

            if (settings.LoadSkyLight && nbtData.TryGetValue("SkyLight", out Tag skyLight)) {
                section.SkyLight = (sbyte[])skyLight;
            }

            if (settings.LoadBlockLight && nbtData.TryGetValue("BlockLight", out Tag blockLight)) {
                section.BlockLight = (sbyte[])blockLight;
            }
        }
        private void LoadPalette(Section section, ChunkReaderArgs<SectionReaderArgs> parameter) {
            PreFlattenedPaletteReader reader = new PreFlattenedPaletteReader(ID.Translator);

            section.BlockStates = new long[0];

            reader.Read(parameter, out Palette palette, out IBlockStateUnlocker unlocker);

            section.Palette = palette;
            section.BlockStateUnlocker = unlocker;
        }
    }
}
