using System;

namespace WorldEditor {
    public interface IRegionReader<TParameter> {
        bool TryRead(TParameter parameter, out Region region);
    }
}
