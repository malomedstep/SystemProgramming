using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ExecutionContextDemoWpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();


            //var ec = ExecutionContext.Capture();
            //ExecutionContext.Run(ec, _ => {

            //}, null);

            //// OR

            //var sc = SynchronizationContext.Current;
            //sc.Post(_ => {

            //}, null);



            // DispatcherSynchronizationContext a;

            // var c = SynchronizationContext.Current;
            //ExecutionContext a;
            //CallContext call;
            //SynchronizationContext sync;
            //SecurityContext sec;
            //ThreadLocal<int> b;
        }

        //private async void Button_Click(object sender, RoutedEventArgs e) {
        //    // SynchronizationContext.SetSynchronizationContext(null);
        //    textBox.Text += $"Hello from thread {Thread.CurrentThread.ManagedThreadId}\n\n";
        //    await Task.Delay(3000);
        //    textBox.Text += $"Hello from thread {Thread.CurrentThread.ManagedThreadId}\n\n";
        //}

        //private void Button_Click(object sender, RoutedEventArgs e) {
        //    // SynchronizationContext.SetSynchronizationContext(null);
        //    textBox.Text += $"Hello from thread {Thread.CurrentThread.ManagedThreadId}\n\n";
        //    var context = SynchronizationContext.Current;
        //    Task.Delay(3000).ContinueWith(task => {
        //        context.Post(_ => {
        //            textBox.Text += $"Hello from thread {Thread.CurrentThread.ManagedThreadId}\n\n";
        //        }, null);
        //    });
        //}

        private void Button_Click(object sender, RoutedEventArgs e) {
            textBox.Text += $"Hello from thread {Thread.CurrentThread.ManagedThreadId}\n\n";
            var context = SynchronizationContext.Current;
            Task.Delay(3000).ContinueWith(task => {
                context.Post(_ => {
                    textBox.Text += $"Hello from thread {Thread.CurrentThread.ManagedThreadId}\n\n";
                }, null);
            });
        }
    }
}
