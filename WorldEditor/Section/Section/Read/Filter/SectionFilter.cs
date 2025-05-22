using System;

namespace WorldEditor {
    public class SectionFilter : ISectionFilter {
        public bool Filter(Section section, SectionLoadSettings sectionLoadSettings) {
            if (section.IsOnlyAir()) {
                switch (sectionLoadSettings.IgnoreOptions) {
                    case SectionIgnoreOptions.IgnoreEmpty:
                        return false;
                    case SectionIgnoreOptions.KeepLight:
                        section.Palette = null;
                        section.BlockStates = null;
                        break;
                }
            }

            return true;
        }
    }
}
