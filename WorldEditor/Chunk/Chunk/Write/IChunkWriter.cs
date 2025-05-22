using System;

namespace WorldEditor {
    public interface IChunkWriter<TParameter> {
        void Write(Chunk chunk, TParameter parameter);
    }
}
