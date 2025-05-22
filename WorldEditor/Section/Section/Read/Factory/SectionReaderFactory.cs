using System;

namespace WorldEditor {
    public class SectionReaderFactory : ISectionReaderFactory<ChunkReaderArgs<SectionReaderArgs>> {
        public ISectionReader<ChunkReaderArgs<SectionReaderArgs>> CreateReader(ChunkReaderArgs args) {
            switch (args.Chunk.DataVersion) {
                case int n when n < 1451:
                    return CreatePreFlattenedReader(args);
                default:
                    return CreateFlattenedReader(args);
            }
        }

        protected virtual ISectionReader<ChunkReaderArgs<SectionReaderArgs>> CreateFlattenedReader(ChunkReaderArgs args) {
            return new FlattenedSectionReader();
        }
        protected virtual ISectionReader<ChunkReaderArgs<SectionReaderArgs>> CreatePreFlattenedReader(ChunkReaderArgs args) {
            return new PreFlattenedSectionReader();
        }
    }
}
