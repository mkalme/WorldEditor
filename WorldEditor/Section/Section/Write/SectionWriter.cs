using System;
using NbtEditor;

namespace WorldEditor {
    public class SectionWriter : ISectionWriter<Container<CompoundTag>> {
        public void Write(Section section, Container<CompoundTag> container) {
            CompoundTag tag = new CompoundTag();

            tag.Add("Y", section.Y);

            if (section.Palette != null) tag.Add("Palette", section.Palette.ToNbt());
            if (section.BlockStates != null) tag.Add("BlockStates", section.BlockStates);
            if (section.SkyLight != null) tag.Add("SkyLight", (sbyte[])(Array)section.SkyLight.Bytes);
            if (section.BlockLight != null) tag.Add("BlockLight", (sbyte[])(Array)section.BlockLight.Bytes);

            container.Value = tag;
        }
    }
}
