using System;
using System.Collections;
using System.Linq;
using NbtEditor;

namespace WorldEditor {
    public class Block : IBlock {
        public string Name { get; set; }
        public Property[] Properties { get; set; }

        public Block(string name, int capacity = 0) {
            Name = name;
            Properties = new Property[capacity];
        }

        //Deserialize
        public static IBlock FromNbt(CompoundTag tag) {
            CompoundTag properties = null;
            int count = 0;

            if (tag.TryGetValue("Properties", out Tag propertyTag)) {
                properties = propertyTag as CompoundTag;
                count = properties.Count;
            }

            string name = tag["Name"];
            IBlock output;

            if (ID.Block.TryGetID(name, out ushort id)) {
                output = new IdBlock(id, count);
            } else {
                output = new Block(name, count);
            }

            for (int i = 0; i < count; i++) {
                var pair = properties.ElementAt(i);

                output.Properties[i] = new Property(pair.Key, pair.Value);
            }

            return output;
        }

        //Default Methods
        public bool Equals(IBlock other) {
            if (Name != other.Name) return false;
            return Properties.Compare(other.Properties);
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return Properties.GetEnumerator();
        }
        public object Clone() {
            Block block = new Block(Name, Properties.Length);

            for (int i = 0; i < Properties.Length; i++) {
                block.Properties[i] = (Property)Properties[i].Clone();
            }

            return block;
        }
    }
}
