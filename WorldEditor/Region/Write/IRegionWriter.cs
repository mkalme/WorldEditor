using System;

namespace WorldEditor {
    public interface IRegionWriter<TParameter> {
        void Write(Region region, TParameter parameter);
    }
}
