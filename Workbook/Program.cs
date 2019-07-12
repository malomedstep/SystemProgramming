using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace Workbook {
    class Program {





        static List<int> numbers = new List<int>();
        static void Foo() {
            for (int i = 0; i < 1000; ++i) {
                lock (numbers) {
                    numbers.Add(i);
                }
            }
        }
        static void Main(string[] args) {
            List<int> a = new List<int> {
                5,6,7,8
            };

            var enumerator = a.GetEnumerator();
            int i = 0;
            var version = a.GetType().GetField("_version", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(a);
            while (enumerator.MoveNext()) {
                if (i == 2) {
                    a.Add(42);
                    a.GetType().GetField("_version", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(a, version);
                }
                Console.WriteLine(enumerator.Current);
                ++i;
            }





            //var hc = new HttpClient();
            //var cts = new CancellationTokenSource();
            //hc.GetAsync("https://google.com", cts.Token);
            //cts.CancelAfter(10000);







            //var cts = new CancellationTokenSource();
            //var token = cts.Token;

            //var task = Task.Run(() => {
            //    Thread.Sleep(1000);
            //    token.ThrowIfCancellationRequested();
            //}, token).ContinueWith(t => {
            //    Console.WriteLine(t.Status);
            //});
            //cts.CancelAfter(10000);
            //// cts.Cancel();
            //Console.ReadLine();









            //var t1 = new Thread(Foo);
            //var t2 = new Thread(Foo);
            //t1.Start();
            //t2.Start();
            //t1.Join();
            //t2.Join();
            //Console.WriteLine(numbers.Count);
        }
    }
}
