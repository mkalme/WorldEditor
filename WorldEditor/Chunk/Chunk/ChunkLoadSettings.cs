using System;

namespace WorldEditor {
    public class ChunkLoadSettings {
        public bool LoadBiomes { get; set; } = true;
        public SectionLoadSettings Section { get; set; } = SectionLoadSettings.Default;
        public HeightmapLoadSettings Heightmap { get; set; } = HeightmapLoadSettings.Default;

        public static readonly ChunkLoadSettings Default = new ChunkLoadSettings();
    }
}
