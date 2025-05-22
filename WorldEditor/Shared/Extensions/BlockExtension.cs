using System;
using NbtEditor;

namespace WorldEditor {
    static class BlockExtension {
        public static CompoundTag ToNbt(this IBlock block) {
            CompoundTag tag = new CompoundTag();

            tag.Add("Name", block.Name);

            Property[] prop = block.Properties;
            if (prop.Length > 0) {
                CompoundTag properties = new CompoundTag();

                for (int i = 0; i < prop.Length; i++) {
                    properties.Add(prop[i].Name, prop[i].Value);
                }

                tag.Add("Properties", properties);
            }

            return tag;
        }
        public static bool Compare(this Property[] left, Property[] right) {
            if (left.Length != right.Length) return false;

            for (int i = 0; i < left.Length; i++) {
                if (!left[i].Equals(right[i])) return false;
            }

            return true;
        }
    }
}
