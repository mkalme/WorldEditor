using System;

namespace WorldEditor {
    public interface IPaletteUnlockerFactory {
        IPaletteUnlocker CreateUnlocker(Chunk chunk);
    }
}
