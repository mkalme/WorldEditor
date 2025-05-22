using System;

namespace WorldEditor {
    public interface ISectionReaderFactory<TSectionParameter> {
        ISectionReader<TSectionParameter> CreateReader(ChunkReaderArgs args);
    }
}
