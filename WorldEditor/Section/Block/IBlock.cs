using System;
using System.Collections;

namespace WorldEditor {
    public interface IBlock : IEquatable<IBlock>, IEnumerable, ICloneable {
        string Name { get; set; }
        Property[] Properties { get; set; }
    }
}
