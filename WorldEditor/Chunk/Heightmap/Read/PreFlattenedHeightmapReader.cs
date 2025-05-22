using System;
using NbtEditor;

namespace WorldEditor {
    public class PreFlattenedHeightmapReader : IHeightmapReader<(Chunk, ArrayTag)> {
        public bool TryRead((Chunk, ArrayTag) parameter, out ushort[] heightmap) {
            int[] heightmapInput = parameter.Item2;

            heightmap = new ushort[heightmapInput.Length];
            for (int i = 0; i < heightmapInput.Length; i++) {
                heightmap[i] = (ushort)heightmapInput[i];
            }

            return true;
        }
    }
}
