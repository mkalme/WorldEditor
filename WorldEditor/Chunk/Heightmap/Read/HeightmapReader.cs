using System;
using NbtEditor;

namespace WorldEditor {
    public class HeightmapReader : IHeightmapReader<(Chunk, ArrayTag)> {
        public IHeightmapUnlockerFactory HeightmapUnlockerFactory { get; set; }

        public HeightmapReader() {
            HeightmapUnlockerFactory = new HeightmapUnlockerFactory();
        }

        public bool TryRead((Chunk, ArrayTag) parameter, out ushort[] map) {
            Chunk chunk = parameter.Item1;
            ArrayTag array = parameter.Item2;

            if (array == null || array.ID != TagID.Int64Array) {
                map = null;
                return false;
            }

            map = HeightmapUnlockerFactory.CreateUnlocker(chunk).Unlock(array);

            return true;
        }
    }
}
