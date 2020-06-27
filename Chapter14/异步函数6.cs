using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 异步函数6
    {
        /*
         *添加了async修饰符的方法称为异步函数，这是因为它们本身也是异步的。
         *当遇到await表达式时，通常情况下执行过程会返回到调用者上。就像迭代器中的yield return
         *一样。但是，运行时在返回之前会在等待的任务上附加一个延续，保证任务结束时，执行点会跳回到方法中，
         *并继续执行剩余的代码。如果任务出错，则会重新抛出异常。如果顺利结束，则返回值为await表达式赋值。
         *
         *
         */
        public static void Invoker()
        {
            /*等待*/
            //var task = NotWait();
            //Console.WriteLine("++++++++++++++++++++");
            //var task2 = Wait();

            {
                //获取本地状态
                /*
                 *await表达式最大的优势在于它几乎可以出现在代码的任意位置。
                 *await表达式可以在任何表达式（异步函数）中出现但不能出现在lock表达式中，unsafe上下文中或者执行入口（Main）中。
                 *
                 *编译器会使用延续在await表达式之后恢复执行（使用awaiter模式）。
                 *这意味着如果代码运行在富客户端应用程序的UI线程上，则同步上下文会将执行恢复到同一个线程上。
                 *否则，执行过程会恢复到任务所在的线程上。线程的更换不会影响执行顺序，但如果设置了线程亲和性，则可能受到影响。
                 *整个过程就像坐出租车浏览某个城市一样。在同步上下文中，代码总是使用同一辆出租车，而没有同步上下文的情况下，
                 *每一次都会使用不同的出租车，但无论使用那种情况，旅程都是相同的。
                 */
                //Display();
                //取消异步操作
                //var cancelSource = new CancellationTokenSource(5000);
                //Foo(cancelSource.Token);
                //cancelSource.Cancel();
                //Task.Run(() =>
                //{
                //    for (int i = 0; i < 10; i++)
                //    {
                //        Console.WriteLine(i);
                //        Thread.Sleep(1000);
                //    }
                //}, cancelSource.Token);


                //进度报告
                //Action<int> action = i => Console.WriteLine(i + "%");
                //FooProcess(action);

                //var process = new Progress<int>(i=> Console.WriteLine(i + "%"));
                var process = new Progress<int>();
                FooProcess(process);
                process.ProgressChanged += Process_ProgressChanged;
                
            }
        }

        private static void Process_ProgressChanged(object sender, int e) => Console.WriteLine(e + "%");

        static Task FooProcess(IProgress<int>  progress)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 10 == 0) progress.Report(i / 10);
                }
            });
        }
        static  Task FooProcess(Action<int> action)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 10 == 0) action(i / 10);
                }
            });
        }


        static async Task Foo(CancellationToken cancellation)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
               await Task.Delay(1000, cancellation);
            }
        }
        static async void Display()
        {
            //在第一次执行wait方法时，由于出现了await表达式，因此执行点返回给调用者。当方法完成活出错时，执行点会从停止之处回复执行，同时保留本地变量的循环计数器的值。
            for (int i = 0; i < 10; i++)
            {
                await Wait();
            }
        }
        private static Task NotWait()
        {
            Console.WriteLine("不使用await关键字");
            var task = Task.Run<int>(() =>
            {
                Console.WriteLine("Hello Word");
                return 0;
            });
            var awaiter = task.GetAwaiter();
            //附加一个延续
            awaiter.OnCompleted(() =>
            {
                var result = awaiter.GetResult();
                Console.WriteLine(result);
            });
            return task;
        }


        private static async Task Wait()
        {
            var i = await Task.Run(() =>
            {
                Console.WriteLine("使用await关键字");
                return 0;
            });
        }
    }
}
