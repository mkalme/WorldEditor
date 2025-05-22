using System;

namespace WorldEditor {
    public class Container<TValue> {
        public TValue Value { get; set; }

        public static implicit operator Container<TValue>(TValue value) => new Container<TValue>(value);
        public static implicit operator TValue(Container<TValue> container) => container.Value;

        public Container() {

        }
        public Container(TValue value) {
            Value = value;
        }
    }
}
