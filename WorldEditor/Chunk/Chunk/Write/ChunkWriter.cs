using System;
using NbtEditor;

namespace WorldEditor {
    public class ChunkWriter : IChunkWriter<Container<byte[]>> {
        public ISectionWriter<Container<CompoundTag>> SectionWriter { get; set; }

        public ChunkWriter() {
            SectionWriter = new SectionWriter();
        }

        public void Write(Chunk chunk, Container<byte[]> container) {
            CompoundTag root = new CompoundTag();
            CompoundTag level = new CompoundTag();

            level.Add("xPos", chunk.X);
            level.Add("zPos", chunk.Z);
            level.Add("Status", chunk.Status);
            level.Add("LastUpdate", chunk.LastUpdateTick);
            if (chunk.Biomes != null) level.Add("Biomes", chunk.Biomes.Biomes);

            ListTag sections = new ListTag(TagID.Compound);
            for (int i = 0; i < chunk.Sections.Count; i++) {
                if (chunk.Sections[i].IsOnlyAir()) continue;

                var sectionContainer = new Container<CompoundTag>();
                SectionWriter.Write(chunk.Sections[i], sectionContainer);

                sections.Add(sectionContainer.Value);
            }
            level.Add("Sections", sections);

            root.Add("DataVersion", chunk.DataVersion);
            root.Add("Level", level);

            container.Value = root.ToBytes();
        }
    }
}
