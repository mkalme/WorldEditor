using System;

namespace WorldEditor {
    public class OldHeightmapUnlocker : IHeightmapUnlocker {
        public ushort[] Unlock(long[] array) {
            return BlockStateHelper.Unlock(array, 9);
        }
    }
}
