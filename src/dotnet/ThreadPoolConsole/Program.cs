using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolConsole {
    class Program {
        static int LinqLikeCount(
            IEnumerable<int> numbers,
            Predicate<int> pred) {
            int c = 0;
            var enumerator = numbers.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (pred.Invoke(enumerator.Current))
                    c++;
            }

            //foreach (var number in numbers) {
            //    if (pred.Invoke(number)) c++;
            //}

            return c;
        }

        class Lambda1 {
            public string name;
            public bool Invoke(string n) {
                return n.Equals(name);
            }
        }


        class Func1 {
            public int variable;
            public int Invoke() {
                return variable * 2;
            }
        }

        static void Main(string[] args) {
            //List<Func<int>> actions = new List<Func<int>>();
            //Func1 myFunc = new Func1(); // ex lambda

            //myFunc.variable = 0;
            //while (myFunc.variable < 5) {
            //    actions.Add(myFunc.Invoke);
            //    myFunc.variable++;
            //}

            //foreach (var act in actions) {
            //    Console.WriteLine(act.Invoke());
            //}



            List<Func<int>> actions = new List<Func<int>>();

            int variable = 0;
            while (variable < 5) {
                int a = variable;
                actions.Add(() => {
                    return a * 2;
                });
                variable++;
            }

            foreach (var act in actions) {
                Console.WriteLine(act.Invoke());
            }




            //var names = new[] {
            //    "John", "Jack", "Joe", "Jake",
            //    "John", "Jinn", "Jeremy", "John"
            //};
            //Console.WriteLine("Enter length: ");

            //var l = int.Parse(Console.ReadLine()); // 4

            //Func<string, bool> lambda = n => {
            //    return n.Length > l;
            //};

            //l = 0;
            //int c = names.Count(lambda);
            //Console.WriteLine(c);




            //var nums = new List<int>(Enumerable.Range(1, 10000));
            //var count = 0;
            //for(int i = 0; i < nums.Count; ++i) {
            //    if (nums[i] % 2 == 0) count++;
            //}
            ////var count = LinqLikeCount(nums, n => n % 2 == 0);

            //Console.WriteLine(count);



            // Thread
            // ThreadPool
            // System.Threading.Timer => trash. never use (ThreadPool) (out)
            // System.Timers.Timer => ThreadPool (out)
            // System.Windows.Forms.Timer => Contol.Invoke (in)
            // System.Windows.???.DispatcherTimer => Dispatcher.Invoke (in)

            //var a = new System.Timers.Timer();
            //a.Interval = 100;
            //a.Elapsed += (s, e) => {
            //    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(1007);
            //};
            //a.Start();
            //Console.ReadLine();
        }
    }
}
