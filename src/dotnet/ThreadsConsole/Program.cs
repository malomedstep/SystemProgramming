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

        static void ThreadFunction(object param) {

        }

        static void RunSeveralParallelThreads() {
            var thread1 = new Thread(ThreadFunction);
            var thread2 = new Thread(ThreadFunction);
            var thread3 = new Thread(ThreadFunction);
            thread1.Start("AAA");
            thread2.Start("BBB");
            thread3.Start("CCC");

            ExecutionContext.Capture();
            // CallContext.HostContext
            HostExecutionContextManager a;

            
        }

        static void Main(string[] args) {
            Contract.Requires<Exception>(10 > 15);
        }
    }
}
