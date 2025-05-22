using System;
using System.Collections.Generic;

namespace WorldEditor {
    public class HeightmapLoadSettings {
        public IList<string> Types { get; set; } = new List<string>() { "WORLD_SURFACE" };
        public bool LoadHeightmap => Types.Count > 0;

        public static readonly HeightmapLoadSettings Default = new HeightmapLoadSettings();
    }
}
