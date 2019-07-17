using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HooksWpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private LowLevelKeyboardListener _listener;

        public MainWindow() {
            InitializeComponent();

            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;
        }

        private void _listener_OnKeyPressed(object sender, KeyPressedArgs e) {
            TextBox.Text += e.KeyPressed.ToString();
        }

        private void SetHookButton_Click(object sender, RoutedEventArgs e) {
            SetHookButton.IsEnabled = false;
            _listener.HookKeyboard();
            UnsetHookButton.IsEnabled = true;
            indicator.Background = new SolidColorBrush(Colors.Green);
        }

        private void UnsetHookButton_Click(object sender, RoutedEventArgs e) {
            UnsetHookButton.IsEnabled = false;
            _listener.UnhookKeyboard();
            SetHookButton.IsEnabled = true;
            indicator.Background = new SolidColorBrush(Colors.Red);
        }

        private void Window_Closed(object sender, EventArgs e) {
            _listener.UnhookKeyboard();
            Application.Current.Shutdown();
        }
    }
}

