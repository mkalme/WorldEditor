using System;

namespace WorldEditor {
    public interface IHeightmapUnlocker {
        ushort[] Unlock(long[] array);
    }
}
