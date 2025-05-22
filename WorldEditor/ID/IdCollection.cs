using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace WorldEditor {
    public sealed class IdCollection<TKey, TId> : IIdCollection<TKey, TId> {
        private IDictionary<TKey, TId> _ids;
        private IDictionary<TId, TKey> _keys;

        public ICollection<TKey> Keys => _ids.Keys;
        public ICollection<TId> IDs => _ids.Values;

        public TId this[TKey key] {
            get {
                return _ids[key];
            }
            set {
                _ids[key] = value;
            }
        }
        public TKey this[TId id] {
            get {
                return _keys[id];
            }
            set {
                _keys[id] = value;
            }
        }

        public int Count => _ids.Count;

        public IdCollection(IDictionary<TKey, TId> ids) {
            _ids = new Dictionary<TKey, TId>(ids);

            _keys = new Dictionary<TId, TKey>(ids.Count);
            for (int i = 0; i < _ids.Count; i++) {
                var pair = ids.ElementAt(i);

                _keys.Add(pair.Value, pair.Key);
            }
        }

        public bool Contains(TKey key) {
            return _ids.ContainsKey(key);
        }
        public bool TryGetID(TKey key, out TId id) {
            return _ids.TryGetValue(key, out id);
        }

        public bool Contains(TId id) {
            return _keys.ContainsKey(id);
        }
        public bool TryGetKey(TId id, out TKey key) {
            return _keys.TryGetValue(id, out key);
        }

        public IEnumerator<KeyValuePair<TKey, TId>> GetEnumerator() {
            return _ids.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public static IdCollection<string, TId> FromFile(string file) {
            JObject biomeObj = JObject.Parse(File.ReadAllText(file));

            return FromJObject(biomeObj["Namespaces"] as JArray);
        }
        public static IdCollection<string, TId> FromJObject(JArray namespaces) {
            Dictionary<string, TId> ids = new Dictionary<string, TId>();

            for (int i = 0; i < namespaces.Count; i++) {
                JObject namespaceObj = namespaces[i] as JObject;

                string nameSpace = (string)namespaceObj["Namespace"];
                JArray idArray = (JArray)namespaceObj["IDs"];

                for (int j = 0; j < idArray.Count; j++) {
                    JArray valueArray = (JArray)idArray[j];

                    string key = $"{nameSpace}:{valueArray[0]}";
                    TId id = valueArray[1].Value<TId>();

                    ids.Add(key, id);
                }
            }

            return new IdCollection<string, TId>(ids);
        }
    }
}
