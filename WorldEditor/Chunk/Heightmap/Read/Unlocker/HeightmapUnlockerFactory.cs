using System;

namespace WorldEditor {
    public class HeightmapUnlockerFactory : IHeightmapUnlockerFactory {
        public IHeightmapUnlocker CreateUnlocker(Chunk chunk) {
            if (chunk.DataVersion < 2529) return new OldHeightmapUnlocker();
            
            return new NewestHeightmapUnlocker();
        }
    }
}
