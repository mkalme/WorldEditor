using System;

namespace WorldEditor {
    public class IdTranslatorFactory : IIdTranslatorFactory {
        public IdCatalogReader CatalogReader { get; set; }

        public IdTranslatorFactory(string fileName) {
            CatalogReader = new IdCatalogReader(fileName);
        }

        public IIdTranslator CreateTranslator() {
            return new IdTranslator(CatalogReader.ReadCatalog());
        }
    }
}
