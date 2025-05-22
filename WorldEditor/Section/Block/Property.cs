using System;

namespace WorldEditor {
    public class Property : IEquatable<Property>, ICloneable {
        public string Name { get; set; }
        public string Value { get; set; }

        public Property(string name, string value) {
            Name = name;
            Value = value;
        }

        //Default Methods
        public bool Equals(Property other) {
            return Name == other.Name && Value == other.Value;
        }
        public object Clone() {
            return new Property(Name, Value);
        }
    }
}
