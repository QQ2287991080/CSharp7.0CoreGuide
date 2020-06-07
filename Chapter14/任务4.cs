using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 任务4
    {
        /*
         *
         */

        public static void Invoker()
        {
            //new Task(() => Console.WriteLine("")).Start();//冷启动

            {
                var task = Task.Run(() => Console.WriteLine());//热启动
                task.Wait();
                //wait方法可以阻塞当前方法，知道任务完成类似线程的join方法。

            }

            /*
             *默认情况下，CLR会将任务运行在线程池线程上 ，这种线程非常适合执行短小的计算密集任务。
             */
             //长任务
            {
                var task = Task.Factory.StartNew(() => Console.WriteLine("避免使用线程池线程。"), TaskCreationOptions.LongRunning);

                /*
                 *在线程池上运行一个长时间执行的任务并不会造成问题。
                 * 但是如果并行运行多个长时间的任务（特别是会造成阻塞的任务），则会造成性能影响。
                 * 相对于使用TaskCreationOptions.LongRunning而言有更好的方案。
                 * 如果运行的是I/O密集型任务，则使用TaskCompletionSource和异步函数通过回调函数而非使用线程实现并发性。
                 * 如果是计算密集型任务，则使用生产者/消费队列可以控制这些任务造成的并发数量，避免出现线程和进程饥饿的问题。
                 */
            }
            {
                Task<int> task = Task.Run(() => Enumerable.Range(2, 3000000).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

                Console.WriteLine(task.Result);
            }
            {
                /*
                 *使用Task的IsFaulted和IsCanceled属性可以在不抛出异常的情况下检查出错误的任务，如果这两个属性都返回false则说明没有错误发生。
                 */
                //OperationCanceledException  IsCanceled=true;
                //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            }
            {
                /*
                 *延续
                 */
                var task = Task.Run(() => Console.WriteLine("延续"));
                var awaiter = task.GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    Console.WriteLine("附加任务");
                });
                //当task执行时，调用任务的awaiter方法的OnCompleted，告诉task你执行完之后还有个一任务需要执行（通过委托附加到task上）
                //如果执行task的时候报错了，当调用GetAwaiter方法时将会重新抛出异常，并且得到的异常是最原始的。

                //如果提供了上下文，OnCompleted将会自动捕获，并将延续提交到这个上下文。这一定非常重要，因为将延续提交到上下文的话，会使得延续就在
                //task这个线程上执行，从而避免了切换线程造成的性能损耗。
                //如果没有上下文的时候
                var xx = task.ConfigureAwait(false).GetAwaiter();

                //另一种延续方式：ContinueWith-----ContinueWith本身返回一个Task对象，所以它很适合多个延续的场景。
                task.ContinueWith(p =>
                {
                    Console.WriteLine("ContinueWith延续");
                }).ContinueWith(p =>

                    Console.WriteLine("延续第二个")
               );

            }
        }

        /// <summary>
        /// 另一种创建任务的方法 TaskCompletionSource 
        /// </summary>
        public static void InvokerSource()
        {
            /*
             *TaskCompletionSource可以创建一个任务，但是这种任务并非那种需要执行启动操作并在随后停止的任务；
             *而是在操作结束或出错时手动创建的“附属任务”。这种非常使用于I/O密集型的工作。它不但可以利用任务所有
             *的优点（能够传递返回值、异常或延续）而且不需要在操作执行期间阻塞线程。
             *
             *
             *TaskCompletionSource的真正作用是创建一个不绑定线程的任务。
             */
            //五秒钟后输入42
            {
                var tsc = new TaskCompletionSource<int>();

                new Thread(() => { Thread.Sleep(5000); tsc.SetResult(42); })
                {
                    IsBackground = true
                }.Start();

                var task = tsc.Task;
                Console.WriteLine(task.Result);
            }
            {
                ///自定义Task.Run方法

                //CustomerRun<int>(() => { Thread.Sleep(5000); throw new NotSupportedException("不支持当前方法"); });
                //自定义Delay方法
                CustomerDelay(5000).ContinueWith(p => Console.WriteLine(42));
            }

        }
       static Task<TResult> CustomerRun<TResult>(Func<TResult> func)
        {
            var tsc = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    tsc.SetResult(func());
                }
                catch (Exception ex)
                {
                    tsc.SetException(ex);
                }
            }).Start();
            return tsc.Task;
        }
        static Task CustomerDelay(int seconds)
        {
            var tsc = new TaskCompletionSource<object>();
            var timer = new System.Timers.Timer(seconds) { AutoReset = true };
            timer.Elapsed += delegate { timer.Dispose(); tsc.SetResult(null); };
            timer.Start();
            return tsc.Task;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
