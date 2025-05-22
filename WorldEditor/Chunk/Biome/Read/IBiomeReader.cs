using System;

namespace WorldEditor {
    public interface IBiomeReader<TParameter> {
        bool TryRead(TParameter parameter, out IBiomeChunk biome);
    }
}
