using System;

namespace WorldEditor {
    public interface ISectionWriter<TParameter> {
        void Write(Section section, TParameter parameter);
    }
}
