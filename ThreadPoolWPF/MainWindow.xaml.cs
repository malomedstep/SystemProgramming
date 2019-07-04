using System.Windows;

namespace ThreadPoolWPF {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            DataContext = new MainViewModel(new WpfDialogService());
        }
    }
}
