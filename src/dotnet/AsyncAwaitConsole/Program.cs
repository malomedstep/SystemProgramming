using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitConsole {
    class Program {






        static async void FooAsync() {
            var wc = new WebClient();
            var url = "https://facebook.com";
            var result = await wc.DownloadStringTaskAsync(url);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);


        }

        static void Foo() {
            var wc = new WebClient();
            var url = "https://facebook.com";
            wc.DownloadStringTaskAsync(url)
                .ContinueWith(t => {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }


        //public static async Task<int> SimpleBodyAsync() {
        //    //Console.WriteLine("Hello, Async World!");
        //    //var wc = new WebClient();
        //    //var url = "https://facebook.com";
        //    //var result = await wc.DownloadStringTaskAsync(url);
        //    //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        //    //return 42;
        //}

        static Task<int> Calc(int a, int b) {
            return Task.FromResult(a + b);
        }

        static async Task<string> AsyncMethod() {
            // var a = await Calc(5, 10);

            var number = int.Parse(Console.ReadLine());
            await Task.Delay(2000);
            var wc = new WebClient();
            await Task.Delay(2000);
            var url = "https://facebook.com";
            await Task.Delay(2000);
            var result = await Task.Run(() => {
                return wc.DownloadString(url);
            });
            //var result = await wc.DownloadStringTaskAsync(url);
            await Task.Delay(2000);
            return result;
        }


        static Task<string> AsyncMethod1() {
            var number = int.Parse(Console.ReadLine());
            return Task.Delay(0).ContinueWith(t => {
                var wc = new WebClient();
                var url = "https://facebook.com";
                return wc.DownloadStringTaskAsync(url)
                .ContinueWith(t2 => {
                    return t2.Result;
                }).Result;
            });
        }




        static void foo() {

        }




        //    [DebuggerStepThrough]
        //    public static Task SimpleBodyAsyncGenerated() {
        //        SimpleBodyAsyncStruct d = new SimpleBodyAsyncStruct();
        //        d._builder = AsyncTaskMethodBuilder<int>.Create();
        //        d.MoveNext();
        //        return d._builder.Task;
        //    }



        //    [CompilerGenerated]
        //    [StructLayout(LayoutKind.Sequential)]
        //    private struct SimpleBodyAsyncStruct : <>t__IStateMachine {   
        //        private int _state;   
        //        public AsyncTaskMethodBuilder<int> _builder;
        //        public Action _MoveNextDelegate;
        //        public WebClient wc;
        //        public string result;
        //        public string url;
        //        public Task<string> _task1;

        //        public void MoveNext() {
        //            try {
        //                if (this._state == -1) 
        //                        return;
        //                if (this._state == 0) {
        //                    Console.WriteLine("Hello, Async World!");
        //                    wc = new WebClient();
        //                    url = "https://facebook.com";
        //                    _task1 = wc.DownloadStringTaskAsync(url);
        //                    if (_task1.IsCompleted) {
        //                        result = _task1.Result;
        //                    } else {
        //                        this._state = 1;
        //                        MoveNext();
        //                    }
        //                } else if (this._state == 1) {

        //                }
        //            } catch (Exception e) {
        //                this._state = -1;
        //                this._builder.SetException(e);
        //                return;
        //            }
        //            this._state = -1;
        //            this._builder.SetResult(42);
        //        }




        //    //[DebuggerStepThrough]
        //    //public static Task SimpleBodyAsync() {   
        //    //    <SimpleBodyAsync>d__0 d__ = new <SimpleBodyAsync>d__0();
        //    //    d__.<> t__builder = AsyncTaskMethodBuilder.Create();
        //    //    d__.MoveNext();
        //    //    return d__.<> t__builder.Task;
        //    //}

        //    //[CompilerGenerated]
        //    //[StructLayout(LayoutKind.Sequential)]
        //    //private struct <SimpleBodyAsync>d__0 : <>t__IStateMachine {   
        //    //    private int <>1__state;   
        //    //    public AsyncTaskMethodBuilder <>t__builder;
        //    //    public Action <>t__MoveNextDelegate;

        //    //    public void MoveNext() {
        //    //        try {
        //    //            if (this.<>1__state == -1) 
        //    //                return;
        //    //            Console.WriteLine("Hello, Async World!");
        //    //        } catch (Exception e) {
        //    //            this.<>1__state = -1;
        //    //            this.<>t__builder.SetException(e);
        //    //            return;
        //    //        }
        //    //        this.<> 1__state = -1;
        //    //        this.<> t__builder.SetResult(42);
        //    //    }     
        //}




        static async Task<int> Get42() {
            await Task.Delay(5000);
            return 42;
        }

        static async Task ImranAsync() {
            var result = await Get42();
            Console.WriteLine(result);
        }

        static async Task Imran2Async() {
            await Task.Delay(1000);
            Console.WriteLine("Ya sdelal");
        }
        static async Task MainAsync(string[] args) {
            await ImranAsync();
            await Imran2Async();
            Console.WriteLine("ngghf");
            Console.ReadLine();
        }
        static void Main(string[] args) {
            var wc = new WebClient();
            var data = wc.DownloadStringTaskAsync(
                "https://facebook.com").Result;
            Console.WriteLine(data);





            MainAsync(args).Wait();

            // string facebook = await AsyncMethod();

            //Foo();
            //FooAsync();

            //Console.ReadLine();



            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            //Task.Run(() => {
            //    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(2000);
            //    return 42;
            //}).ContinueWith(t => {
            //    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //}, TaskScheduler.FromCurrentSynchronizationContext());


            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //Console.ReadLine();
        }
    }
}
