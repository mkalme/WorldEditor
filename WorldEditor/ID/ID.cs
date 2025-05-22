using System;

namespace WorldEditor {
    public static class ID {
        public static readonly IIdCollection<string, ushort> Block = IdCollection<string, ushort>.FromFile("ID\\BlockID.json");
        public static readonly IIdCollection<string, int> Biome = IdCollection<string, int>.FromFile("ID\\BiomeID.json");

        public static readonly IIdTranslator Translator = IdTranslator.FromFile("ID\\IdTranslatorCatalog.json");
    }
}
