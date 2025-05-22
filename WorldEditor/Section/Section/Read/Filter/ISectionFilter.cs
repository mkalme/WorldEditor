using System;

namespace WorldEditor {
    public interface ISectionFilter {
        bool Filter(Section section, SectionLoadSettings sectionLoadSettings);
    }
}
