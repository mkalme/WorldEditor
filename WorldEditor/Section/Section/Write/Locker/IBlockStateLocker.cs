using System;

namespace WorldEditor {
    public interface IBlockStateLocker {
        long[] Lock(ushort[] blocks, Palette palette);
    }
}
