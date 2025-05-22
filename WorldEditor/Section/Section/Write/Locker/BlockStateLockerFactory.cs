using System;

namespace WorldEditor {
    public class BlockStateLockerFactory : IBlockStateLockerFactory {
        public IBlockStateLocker CreateLocker(Chunk chunk) {
            if (chunk.DataVersion < 2529) new OldBlockStateLocker();
            
            return new NewestBlockStateLocker();
        }
    }
}
