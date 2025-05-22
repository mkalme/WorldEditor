using System;
using NbtEditor;

namespace WorldEditor {
    public class SectionReaderArgs {
        public CompoundTag NbtData { get; set; }
        public SectionLoadSettings Settings { get; set; }

        public IBlockStateLocker BlockStateLocker { get; set; }
        public IBlockStateUnlocker BlockStateUnlocker { get; set; }
        public IPaletteUnlocker PaletteUnlocker { get; set; }
    }
}
