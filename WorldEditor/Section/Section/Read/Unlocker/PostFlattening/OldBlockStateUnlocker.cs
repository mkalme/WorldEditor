using System;

namespace WorldEditor {
    public class OldBlockStateUnlocker : IBlockStateUnlocker {
        public ushort[] Unlock(long[] blockStates, Palette palette) {
            return BlockStateHelper.Unlock(blockStates, palette.GetMinBitLength());
        }
    }
}
