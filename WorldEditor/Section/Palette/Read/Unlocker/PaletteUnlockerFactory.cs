using System;

namespace WorldEditor {
    public class PaletteUnlockerFactory : IPaletteUnlockerFactory {
        public IPaletteUnlocker CreateUnlocker(Chunk chunk) {
            if (chunk.DataVersion < 2529) return new OldPaletteUnlocker();
            
            return new NewestPaletteUnlocker();
        }
    }
}
