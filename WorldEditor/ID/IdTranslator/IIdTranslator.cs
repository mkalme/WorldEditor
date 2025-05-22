using System;

namespace WorldEditor {
    public interface IIdTranslator {
        bool TryTranslate(ushort id, byte data, out IBlock block);
    }
}
