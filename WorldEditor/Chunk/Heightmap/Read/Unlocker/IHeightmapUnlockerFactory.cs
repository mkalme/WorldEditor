using System;

namespace WorldEditor {
    public interface IHeightmapUnlockerFactory {
        IHeightmapUnlocker CreateUnlocker(Chunk chunk);
    }
}
