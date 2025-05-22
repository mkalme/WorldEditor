using System;
using NbtEditor;

namespace WorldEditor {
    public class BiomeChunkFactory : IBiomeChunkFactory {
        public IBiomeChunk CreateBiomeChunk(Chunk chunk, Tag biomes) {
            if (chunk.DataVersion >= 2203) {
                switch (biomes.ID) {
                    case TagID.Int32Array:
                        return new BiomeChunk3D((int[])biomes);
                    case TagID.ByteArray:
                        return new BiomeChunk3D((sbyte[])biomes);
                }
            } else {
                switch (biomes.ID) {
                    case TagID.Int32Array:
                        return new BiomeChunk2D((int[])biomes);
                    case TagID.ByteArray:
                        return new BiomeChunk2D((sbyte[])biomes);
                }
            }

            return null;
        }
    }
}
