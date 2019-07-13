using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncDotnetConsole {

    class Singleton {
        public event EventHandler Event;
        public void Publish() {
            var subs = Event.GetInvocationList();

            foreach (var item in subs) {
                try {
                    item.Method.Invoke(item.Target, null);
                } catch (Exception ex) {

                    // log some data
                }
            }
        }

        private static Singleton _instance;
        private static readonly object _sync = new object();
        private Singleton() { }
        public static Singleton Instance {
            get {
                lock(_sync) {
                    if (_instance is null) {
                        _instance = new Singleton();
                    }
                    return _instance;
                }
            }
        }
    }


    class Program {
        static object sync = new object();
        static int counter = 0;

        static void ThreadProcedure() {
            for(int i = 0; i < 1000000; ++i) {
                lock (sync) {

                }
            }
        }

        static void Main(string[] args) {
            Singleton.Instance.Event += (s, e) => {

            };
            Singleton.Instance.Publish();



            var t1 = new Thread(ThreadProcedure);
            var t2 = new Thread(ThreadProcedure);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine(counter);
        }
    }
}
