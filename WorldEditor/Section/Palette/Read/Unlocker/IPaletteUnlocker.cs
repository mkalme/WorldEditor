using System;

namespace WorldEditor {
    public interface IPaletteUnlocker {
        IBlock[] UnlockPalette(long[] blockStates, Palette palette);
    }
}
