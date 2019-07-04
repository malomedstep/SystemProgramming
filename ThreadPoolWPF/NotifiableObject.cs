using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ThreadPoolWPF {
    public abstract class NotifiableObject : INotifyPropertyChanged {
        protected void Set<T>(ref T prop, T value, [CallerMemberName]string name = "") {
            prop = value;
            OnPropertyChanged(name);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
