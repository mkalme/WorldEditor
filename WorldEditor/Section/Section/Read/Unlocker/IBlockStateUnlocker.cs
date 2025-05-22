using System;

namespace WorldEditor {
    public interface IBlockStateUnlocker {
        ushort[] Unlock(long[] blockStates, Palette palette);
    }
}
