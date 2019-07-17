using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Workbook {
    //class Program {
    //    static List<int> numbers = new List<int>();
    //    static void Foo() {
    //        for (int i = 0; i < 1000; ++i) {
    //            lock (numbers) {
    //                numbers.Add(i);
    //            }
    //        }
    //    }
    //    static void Main(string[] args) {
    //        List<int> a = new List<int> {
    //            5,6,7,8
    //        };
    //        var enumerator = a.GetEnumerator();
    //        int i = 0;
    //        var version = a.GetType().GetField("_version", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(a);
    //        while (enumerator.MoveNext()) {
    //            if (i == 2) {
    //                a.Add(42);
    //                a.GetType().GetField("_version", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(a, version);
    //            }
    //            Console.WriteLine(enumerator.Current);
    //            ++i;
    //        }





    //        //var hc = new HttpClient();
    //        //var cts = new CancellationTokenSource();
    //        //hc.GetAsync("https://google.com", cts.Token);
    //        //cts.CancelAfter(10000);







    //        //var cts = new CancellationTokenSource();
    //        //var token = cts.Token;

    //        //var task = Task.Run(() => {
    //        //    Thread.Sleep(1000);
    //        //    token.ThrowIfCancellationRequested();
    //        //}, token).ContinueWith(t => {
    //        //    Console.WriteLine(t.Status);
    //        //});
    //        //cts.CancelAfter(10000);
    //        //// cts.Cancel();
    //        //Console.ReadLine();









    //        //var t1 = new Thread(Foo);
    //        //var t2 = new Thread(Foo);
    //        //t1.Start();
    //        //t2.Start();
    //        //t1.Join();
    //        //t2.Join();
    //        //Console.WriteLine(numbers.Count);
    //    }
    //}

    class Demo {
        private int number;

        public int Number {
            get { Console.WriteLine("get " + number); return number; }
            set { number = value; }
        }

        public static bool operator true(Demo demo) {
            Console.WriteLine($"public static bool operator true ({demo.number})");
            return demo.Number != 0;
        }
        public static bool operator false(Demo demo) {
            Console.WriteLine($"public static bool operator false ({demo.number})");
            return demo.Number == 0;
        }

        public static Demo operator &(Demo demo1, Demo demo2) { 
            Console.WriteLine($"public static Demo operator & ({demo1.number} {demo2.number})");
            if (demo1.Number != 0 && demo2.Number != 0) {
                return new Demo {
                    Number = 1
                };
            }
            return new Demo {
                Number = 0
            };
        }

        public static Demo operator |(Demo demo1, Demo demo2) {
            Console.WriteLine($"public static Demo operator | ({demo1.number} {demo2.number})");
            if (demo1.Number != 0 && demo2.Number != 0) {
                return new Demo {
                    Number = 0
                };
            }
            return new Demo {
                Number = 1
            };
        }
    }

    class Program {


        [DllImport("user32.dll")]
        static extern int MessageBox(
            IntPtr hWnd,
            string lpszText,
            string lpszCaption,
            int nButtons
        );
        

        static void Main(string[] args) {
            MessageBox(
                IntPtr.Zero,
                "Shalom",
                "Hello",
                4
           );









            //Demo demo1 = new Demo { Number = 0 };
            //Demo demo2 = new Demo { Number = 1 };

            //if (demo1 && demo2) {
            //    Console.WriteLine("==");
            //} else {
            //    Console.WriteLine("!=");
            //}
        }
    }
}
