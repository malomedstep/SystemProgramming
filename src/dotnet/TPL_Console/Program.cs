using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL_Console {
    class Program {
        static void Main(string[] args) {

            //Task.Run(() => {
            //    var wc = new WebClient();
            //    var str = wc.DownloadString("https://facebook.com");
            //    Console.WriteLine(str);
            //});




            //var t = new Task<int>(() => {
            //    Thread.Sleep(2000);
            //    return 42;
            //});
            //t.Start();


            var wc1 = new WebClient();
            wc1.DownloadStringTaskAsync("https://facebook.com")
              .ContinueWith(t => {
                  Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                  Console.WriteLine(t.Result.Substring(0, 20));
              });

            var wc2 = new WebClient();
            wc2.DownloadStringTaskAsync("https://google.com")
              .ContinueWith(t => {
                  Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                  Console.WriteLine(t.Result.Substring(0, 20));
              });

            Console.ReadLine();
        }
    }
}
