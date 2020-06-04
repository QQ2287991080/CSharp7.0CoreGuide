using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 任务
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

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
