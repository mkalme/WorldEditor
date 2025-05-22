using System;
using System.Collections.Generic;

namespace WorldEditor {
    public interface IIdCollection<TKey, TId> : IReadOnlyCollection<KeyValuePair<TKey, TId>> {
        ICollection<TKey> Keys { get; }
        ICollection<TId> IDs { get; }

        TId this[TKey key] { get; }
        TKey this[TId id] { get; }

        bool Contains(TKey key);
        bool TryGetID(TKey key, out TId id);

        bool Contains(TId id);
        bool TryGetKey(TId id, out TKey key);
    }
}
