using System;

namespace WorldEditor {
    public interface INodeReader<TParameter, TResult> {
        bool TryRead(TParameter parameter, out TResult result);
    }

    public interface ISectionReader<TParameter> {
        bool TryRead(TParameter parameter, out Section section);
    }
}
