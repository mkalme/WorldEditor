using System;
using NbtEditor;

namespace WorldEditor {
    public class FlattenedSectionReader : ISectionReader<ChunkReaderArgs<SectionReaderArgs>> {
        public ISectionFilter SectionFilter { get; set; }

        public FlattenedSectionReader() {
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
            section.BlockStateUnlocker = parameter.Parameter.BlockStateUnlocker;
            section.PaletteUnlocker = parameter.Parameter.PaletteUnlocker;

            LoadLight(section, parameter);
            LoadPalette(section, parameter);
            LoadBlockStates(section, parameter);

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
            if (parameter.Parameter.NbtData.TryGetValue("Palette", out Tag paletteTag)) {
                section.Palette = Palette.FromNbt(paletteTag as ListTag);
            }
        }
        private void LoadBlockStates(Section section, ChunkReaderArgs<SectionReaderArgs> parameter) {
            if (parameter.Parameter.NbtData.TryGetValue("BlockStates", out Tag blockStates)) {
                section.BlockStates = blockStates;
            }
        }
    }
}
