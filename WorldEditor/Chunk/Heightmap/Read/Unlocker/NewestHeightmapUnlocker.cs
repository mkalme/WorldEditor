using System;

namespace WorldEditor {
    public class NewestHeightmapUnlocker : IHeightmapUnlocker {
        public ushort[] Unlock(long[] array) {
            ushort[] columns = new ushort[256];

            int index = 0;
            for (int i = 0; i < array.Length; i++) {
                for (int j = 0; j < 63; j += 9) {
                    if (index >= 256) return columns;

                    columns[index++] = (ushort)array[i].ExtractBits(j, 55);
                }
            }

            return columns;
        }
    }
}
