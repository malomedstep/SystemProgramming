using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace TPL_WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        CancellationTokenSource cts;
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var context = SynchronizationContext.Current;

            cts = new CancellationTokenSource();
            var token = cts.Token;
            Task.Run(() => {
                var wc = new WebClient();
                var data = wc.DownloadString("https://facebook.com");
                var bytes = Encoding.Default.GetBytes(data);

                token.ThrowIfCancellationRequested();

                for (int i = 0; i < bytes.Length; ++i) {
                    bytes[i] = (byte)(bytes[i] ^ 42);
                }

                token.ThrowIfCancellationRequested();

                using (var fs = new FileStream(
                    "file.txt", FileMode.Create)) {
                    fs.Write(bytes, 0, bytes.Length);
                }

                if (token.IsCancellationRequested) {
                    throw new OperationCanceledException();
                }
                token.ThrowIfCancellationRequested();

                context.Post(_ => {
                    textBox.Text = Encoding.Default.GetString(bytes);
                }, null);

                token.ThrowIfCancellationRequested();

                var socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Dgram,
                    ProtocolType.Udp
                );

                var ep = new IPEndPoint(IPAddress.Parse("192.168.0.42"), 4501);
                socket.SendTo(bytes, ep);
            }, token).ContinueWith(t => {
                MessageBox.Show(t.Status.ToString(), "");
            });







            //var wc = new WebClient();

            //var context = SynchronizationContext.Current;

            //wc.DownloadStringTaskAsync("https://google.com")
            //  .ContinueWith(t => {
            //      context.Post(_ => {
            //          this.textBox.Text = t.Result;
            //      }, null);
            //  });

            //var wc = new WebClient();

            //wc.DownloadStringTaskAsync("https://google.com")
            //  .ContinueWith(t => {
            //      this.textBox.Text = t.Result;
            //  }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            cts?.Cancel();
        }
    }
}
