using System;

namespace WorldEditor {
    public interface IBlockStateUnlockerFactory {
        IBlockStateUnlocker CreateUnlocker(Chunk chunk);
    }
}
