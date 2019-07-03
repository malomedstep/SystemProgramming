namespace ThreadPoolWPF {
    public class ActiveProperty<T> : NotifiableObject {
        private T _value;
        public T Value {
            get => _value;
            set => Set(ref _value, value);
        }

        public ActiveProperty() {
            Value = default;
        }
        public ActiveProperty(T value) {
            Value = value;
        }
    }
}
