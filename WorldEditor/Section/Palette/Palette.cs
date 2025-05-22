using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NbtEditor;

namespace WorldEditor {
    public class Palette : IEnumerable, IEquatable<Palette>, ICloneable {
        public IBlock[] Blocks { get; set; }

        public IBlock this[int index] {
            get => Blocks[index];
            set => Blocks[index] = value;
        }

        public Palette(int capacity) {
            Blocks = new IBlock[capacity];
        }
        public Palette(IBlock[] blocks) {
            Blocks = blocks;
        }
        private Palette(IDictionary<IBlock, ushort> blocks) {
            Blocks = new IBlock[blocks.Count];
            for (int i = 0; i < blocks.Count; i++) {
                Blocks[i] = blocks.ElementAt(i).Key;
            }
        }
        private Palette(IDictionary<ushort, ushort> ids) {
            Blocks = new IBlock[ids.Count];
            for (int i = 0; i < ids.Count; i++) {
                Blocks[i] = new IdBlock(ids.ElementAt(i).Key);
            }
        }

        public int GetMinBitLength() {
            int length = (int)Math.Ceiling(Math.Log(Blocks.Length, 2));
            
            return length < 4 ? 4 : length;
        }

        //Deserialize
        public static Palette FromNbt(ListTag tag) {
            Palette palette = new Palette(tag.Count);

            for (int i = 0; i < tag.Count; i++) {
                palette.Blocks[i] = Block.FromNbt(tag[i] as CompoundTag);
            }

            return palette;
        }
        public static Palette FromBlockList(IList<IBlock> blocks, out ushort[] indexes) {
            indexes = new ushort[4096];
            var palette = new Dictionary<IBlock, ushort>();

            for (int i = 0; i < blocks.Count; i++) {
                IBlock block = blocks[i];

                ushort index;
                bool contains = palette.TryGetValue(block, out index);

                if (!contains) {
                    palette.Add(block, (ushort)palette.Count);
                    index = (ushort)(palette.Count - 1);
                }

                indexes[i] = index;
            }

            return new Palette(palette);
        }
        public static Palette FromIdCollection(IList<ushort> ids, out ushort[] indexes) {
            indexes = new ushort[4096];
            var uniqueIds = new Dictionary<ushort, ushort>();

            for (int i = 0; i < ids.Count; i++) {
                ushort index;
                bool contains = uniqueIds.TryGetValue(ids[i], out index);

                if (!contains) {
                    uniqueIds.Add(ids[i], (ushort)i);
                    index = (ushort)(uniqueIds.Count - 1);
                }

                indexes[i] = index;
            }

            return new Palette(uniqueIds);
        }

        //Serialize
        public ListTag ToNbt() {
            ListTag tag = new ListTag(TagID.Compound);

            for (int i = 0; i < Blocks.Length; i++) {
                tag.Add(Blocks[i].ToNbt());
            }

            return tag;
        }

        //Default Methods
        public object Clone() {
            Palette palette = new Palette(Blocks.Length);
            Blocks.CopyTo(palette.Blocks, 0);

            return palette;
        }
        public bool Equals(Palette other) {
            if (other == null) return false;
            if (Blocks.Length != other.Blocks.Length) return false;

            for (int i = 0; i < Blocks.Length; i++) {
                if (!Blocks[i].Equals(other.Blocks[i])) return false;
            }

            return true;
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return Blocks.GetEnumerator();
        }
    }
}
