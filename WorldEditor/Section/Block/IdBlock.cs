using System;
using System.Collections;

namespace WorldEditor {
    public class IdBlock : IBlock, IEquatable<IBlock> {
        public ushort ID { get; set; }
        public string Name {
            get => WorldEditor.ID.Block[ID];
            set {
                ID = WorldEditor.ID.Block[value];
            }
        }

        public Property[] Properties { get; set; }

        public IdBlock(ushort id, int capacity = 0) {
            ID = id;
            Properties = new Property[capacity];
        }

        //Default Methods
        public bool Equals(IBlock other) {
            if (Name != other.Name) return false;
            return Properties.Compare(other.Properties);
        }
        public IEnumerator GetEnumerator() {
            return Properties.GetEnumerator();
        }
        public object Clone() {
            IdBlock block = new IdBlock(ID, Properties.Length);

            for (int i = 0; i < Properties.Length; i++) {
                block.Properties[i] = (Property)Properties[i].Clone();
            }

            return block;
        }
    }
}
