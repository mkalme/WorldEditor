using System;

namespace WorldEditor {
    public interface IIdTranslatorFactory {
        IIdTranslator CreateTranslator();
    }
}
