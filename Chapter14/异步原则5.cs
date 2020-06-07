using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 异步原则5
    {
        /*
         *同步操作：先完场其它工作再返回调用者
         *异步操作：大部分工作则是在返回给调用者之后完成的。
         *
         *tips：什么是异步编程？
         *异步编程的原则是以异步的方式编写运行时间很长的函数。这和编写长时间原型的函数的传统方法刚好相反。
         *它会在一个新的线程或任务上调用这些函数，从而实现并发性。
         *
         *异步方法的不同点在于并发性的长时间运行的方法内启动，而不是在运行的方法外启动。
         *好处：
         *1.I/O密集的并发性的实现并不需要绑定线程。因此可以提高可伸缩性和效率。
         *2.富客户端应用程序可减少工作线程的代码，因此可以简化线程安全想的实现。
         *
         *在传统的同步调用图中，如果出现一个运行时间很长的操作，我们就必须将整个调用图转移到一个工作线程中以保持ui的响应性。
         *因此我们会得到一个跨越很多方法的并发操作（粗粒度并发性），此使需要考虑调用图中的每一个方法的线程安全性。
         *
         *使用异步调用图，就可以在真正需要的时候再启动线程。因此可以降低调用图中的线程使用频率（或者在I/O密集性操作的中完全不使用线程。）
         *其它的方法则可以在ui线程上执行，从而大大简化线程安全性的实现。这种方式称为细粒度的并发性，即由一系列小的并发操作组成，而在这些操作之间插入ui线程的执行过程。
         */

        public static void Invoker()
        {
            //当我i们没有async和await关键字的时候
            var task = DisplayPrimeCountAsync();
            Console.WriteLine();
        }
        static Task DisplayPrimeCountAsync()
        {
            var machine = new PrimeStateMachine();
            machine.DisplayPrimeCountsFrom(0);
            return machine.Task;
        }

    }
    class PrimeStateMachine
    {
        TaskCompletionSource<object> tsc = new TaskCompletionSource<object>();
        public Task Task { get => tsc.Task; }

        public void DisplayPrimeCountsFrom(int i)
        {
            var awaiter = GetPrimeCount(i * 100000 + 2, 1000000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine(awaiter.GetResult());
                if (++i < 10) DisplayPrimeCountsFrom(i);
                else { Console.WriteLine("Over"); tsc.SetResult(null); }
            });
        }

        Task<int> GetPrimeCount(int start,int end)
        {
           return Task.Run(() => Enumerable.Range(start, end).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
    }
}
