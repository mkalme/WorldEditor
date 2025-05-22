using System;

namespace WorldEditor {
    public class BlockStateUnlockerFactory : IBlockStateUnlockerFactory {
        public IBlockStateUnlocker CreateUnlocker(Chunk chunk) {
            if (chunk.DataVersion < 2529) return new OldBlockStateUnlocker();
            
            return new NewestBlockStateUnlocker();
        }
    }
}
