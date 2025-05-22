using System;
using System.Collections.Generic;

namespace WorldEditor {
    public class IdTranslator : IIdTranslator {
        private readonly IDictionary<ushort, IBlock> _idCatalog;

        public IdTranslator(IDictionary<ushort, IBlock> idCatalog) {
            _idCatalog = idCatalog;
        }

        public static IIdTranslator FromFile(string file) {
            IdCatalogReader reader = new IdCatalogReader(file);

            return new IdTranslator(reader.ReadCatalog());
        }

        public bool TryTranslate(ushort id, byte data, out IBlock block) {
            return _idCatalog.TryGetValue(IdHelper.CreateKey(id, data), out block);
        }
    }
}
