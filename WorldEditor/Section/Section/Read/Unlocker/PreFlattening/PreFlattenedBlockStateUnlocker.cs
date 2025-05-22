using System;

namespace WorldEditor {
    public class PreFlattenedBlockStateUnlocker : IBlockStateUnlocker {
        private ushort[] _blocks;

        public PreFlattenedBlockStateUnlocker(ushort[] blocks) {
            _blocks = blocks;
        }

        public ushort[] Unlock(long[] blockStates, Palette palette) {
            return _blocks;
        }
    }
}
