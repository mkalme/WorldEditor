using System;
using Nibble4;

namespace WorldEditor {
    public class Section {
        public sbyte Y { get; set; } = 0;

        public Palette Palette { get; set; }
        public long[] BlockStates { get; set; }
        public Nibble4Array SkyLight { get; set; }
        public Nibble4Array BlockLight { get; set; }

        public IBlockStateLocker BlockStateLocker { get; set; }
        public IBlockStateUnlocker BlockStateUnlocker { get; set; }
        public IPaletteUnlocker PaletteUnlocker { get; set; }

        public Section() { 
            
        }

        public ushort[] Unlock() {
            return BlockStateUnlocker.Unlock(BlockStates, Palette);
        }
        public IBlock[] UnlockPalette() {
            return PaletteUnlocker.UnlockPalette(BlockStates, Palette);
        }

        public void Lock(ushort[] blocks) {
            BlockStates = BlockStateLocker.Lock(blocks, Palette);
        }
        public void LockPalette(IBlock[] blocks) {
            Palette = Palette.FromBlockList(blocks, out ushort[] indexes);

            Lock(indexes);
        }

        public bool ConsistsOfOneBlock(IBlock block) {
            if (Palette == null || BlockStates == null) return false;
            if (Palette.Blocks.Length == 1) return Palette.Blocks[0].Equals(block);

            return false;
        }
        public bool IsOnlyAir() {
            if (Palette == null || BlockStates == null) return false;
            if (Palette.Blocks.Length == 1) return Palette.Blocks[0].Name == "minecraft:air";

            return false;
        }
        public bool IsEmpty() {
            return Palette == null || BlockStates == null;
        }
    }
}
