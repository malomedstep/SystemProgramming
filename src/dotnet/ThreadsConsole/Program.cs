using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsConsole {
    class Program {

        static void ThreadFunction(object a) {

        }

        static void RunSeveralParallelThreads() {
            var thr = new Thread(ThreadFunction) {
                IsBackground = true
            };
            thr.Start(42);

            thr.Abort();
            thr.Join();            
        }

        static void Main(string[] args) {
            
        }
    }
}
