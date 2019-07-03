using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ThreadPoolWPF {

    public interface IDialogService {
        void Info(string text, string caption);
        void Error(string text, string caption);
        bool Confirm(string text, string caption);
    }
    public class WpfDialogService : IDialogService {
        public void Info(string text, string caption) {
            MessageBox.Show(
                Application.Current.MainWindow,
                text,
                caption,
                MessageBoxButton.OK,
                MessageBoxImage.Information,
                MessageBoxResult.OK
            );
        }

        public void Error(string text, string caption) {
            MessageBox.Show(
                Application.Current.MainWindow,
                text,
                caption,
                MessageBoxButton.OK,
                MessageBoxImage.Error,
                MessageBoxResult.OK
            );
        }

        public bool Confirm(string text, string caption) {
            return MessageBox.Show(
                Application.Current.MainWindow,
                text,
                caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Information,
                MessageBoxResult.No
            ) == MessageBoxResult.Yes;
        }
    }
}
