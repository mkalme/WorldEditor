using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WorldEditor {
    public class IdCatalogReader {
        public string FileName { get; set; }

        public IdCatalogReader(string fileName) {
            FileName = fileName;
        }

        public IDictionary<ushort, IBlock> ReadCatalog() {
            JArray ids = JObject.Parse(File.ReadAllText(FileName))["IDs"] as JArray;

            Dictionary<ushort, IBlock> output = new Dictionary<ushort, IBlock>(ids.Count);
            foreach (var id in ids) {
                ushort key = IdHelper.CreateKey((ushort)id[0], (byte)id[1]);
                IBlock block = GetBlock(id as JArray);

                output.Add(key, block);
            }

            return output;
        }

        private static IBlock GetBlock(JArray id) {
            string name = (string)id[2];

            JArray propertyArray = id.Count > 3 ? id[3] as JArray : null;

            Property[] properties = new Property[propertyArray != null ? propertyArray.Count : 0];
            for (int i = 0; i < properties.Length; i++) {
                properties[i] = GetProperty((string)propertyArray[i]);
            }

            if (ID.Block.TryGetID(name, out ushort blockId)) return new IdBlock(blockId) { Properties = properties };

            return new Block(name) { Properties = properties };
        }
        private static Property GetProperty(string property) {
            string[] split = property.Split("=");

            return new Property(split[0], split[1]);
        }
    }
}
