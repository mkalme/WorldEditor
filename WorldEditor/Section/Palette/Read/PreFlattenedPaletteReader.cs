using System;
using System.Collections.Generic;
using NbtEditor;
using Nibble4;

namespace WorldEditor {
    public class PreFlattenedPaletteReader {
        public IIdTranslator IdTranslator { get; set; }

        public PreFlattenedPaletteReader(IIdTranslator translator) {
            IdTranslator = translator;
        }

        public void Read(ChunkReaderArgs<SectionReaderArgs> parameter, out Palette palette, out IBlockStateUnlocker unlocker) {
            byte[] blocks = parameter.Parameter.NbtData["Blocks"].ToByteArray();

            Nibble4Array add = null;
            bool addExists = parameter.Parameter.NbtData.TryGetValue("Add", out Tag addTag);
            if (addExists) add = addTag.ToByteArray();

            Nibble4Array data = parameter.Parameter.NbtData["Data"].ToByteArray();

            var uniqueBlocks = new Dictionary<ushort, ushort>(16);
            ushort[] unlockedBlocks = new ushort[4096];
            List<IBlock> paletteBlocks = new List<IBlock>(16);

            for (int i = 0; i < 4096; i++) {
                ushort id = blocks[i];
                if (addExists) id |= (ushort)(IdHelper.ReadNibble(add, i) << 8);
                byte blockData = IdHelper.ReadNibble(data, i);

                ushort key = IdHelper.CreateKey(id, blockData);

                if (uniqueBlocks.TryGetValue(key, out ushort index)) {
                    unlockedBlocks[i] = index;
                } else {
                    if (IdTranslator.TryTranslate(id, blockData, out IBlock block)) {
                        uniqueBlocks.Add(key, (ushort)uniqueBlocks.Count);
                        paletteBlocks.Add(block);

                        unlockedBlocks[i] = (ushort)(uniqueBlocks.Count - 1);
                    }
                }
            }

            palette = new Palette(paletteBlocks.ToArray());
            unlocker = new PreFlattenedBlockStateUnlocker(unlockedBlocks);
        }
    }
}
