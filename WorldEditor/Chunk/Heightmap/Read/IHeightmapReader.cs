using System;

namespace WorldEditor {
    public interface IHeightmapReader<TParameter> {
        bool TryRead(TParameter parameter, out ushort[] heightmap);
    }
}
