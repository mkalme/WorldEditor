using System;

namespace WorldEditor {
    public interface IBlockStateLockerFactory {
        IBlockStateLocker CreateLocker(Chunk chunk);
    }
}
