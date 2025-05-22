using System;

namespace WorldEditor {
    public interface IChunkReader<TParameter> {
        bool TryRead(TParameter parameter, out Chunk chunk);
    }
}
