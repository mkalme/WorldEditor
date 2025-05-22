using System;

namespace WorldEditor {
    public class SectionLoadSettings {
        public bool LoadSections { get; set; } = true;
        public SectionIgnoreOptions IgnoreOptions { get; set; } = SectionIgnoreOptions.IgnoreEmpty;
        public bool LoadSkyLight { get; set; } = false;
        public bool LoadBlockLight { get; set; } = false;

        public static readonly SectionLoadSettings Default = new SectionLoadSettings();
    }
}
