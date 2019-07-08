using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ThreadPoolWPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        //private Mutex _mutex;
        private Semaphore _semaphore;
        public App() {
            if (Semaphore.TryOpenExisting(
                "semaphore", out _semaphore)) {
                if (!_semaphore.WaitOne(1)) {
                    Environment.Exit(0);
                }
            } else {
                _semaphore = new Semaphore(
                    1, 1, "semaphore"
                );
                _semaphore.WaitOne();
            }
        }
    }
}
