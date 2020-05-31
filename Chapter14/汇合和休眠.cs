using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 汇合和休眠
    {

        public static void Invoker()
        {
            /*
             * Join方法会等待线程结束再执行主线程。
             * Sleep会暂停当前线程，并且能够指定暂停的时间
             * Yield会将资源交给同一个处理上运行的线程。
             * 
             * Thread.Sleep(0)和Thread.Yield在高级性能调优方面非常有用。
             * 同时它还是一种很好的诊断工具，可以帮助开发者发现线程安全相关的问题。
             * 如果在代码的任务位置插入Thread.Yield()导致程序失败，那么代码一定存在缺陷。
             * 
             */
            Thread thread = new Thread(WriteY);
            thread.Start();
            //thread.Join();
            //Thread.Sleep(1000);
            //Thread.Yield();
            
            Console.WriteLine("开始执行主线程！");
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("X");
            }
            Console.WriteLine("主线程执行完毕！！");
            Console.WriteLine(thread.ThreadState);
        }

        public static void ZiXuan()
        {
            int index = 1;
            while (DateTime.Now<new DateTime(2020,5,31,11,57,59))
            {
                Thread.Sleep(100);
                Console.WriteLine(index);
                index++;
            }
            /*
             *一般来说，这样的作法非常浪费处理器时间。因为CLR和操作系统都会认为这个线程正在执行重要的运算。
             * 因此就会为它分配相应的资源，因此从效果上来说我们将一个I/O密集操作转换为计算密集操作。
             */
        }
        /*
         *在等待Sleep或者Join的过程中，线程时阻塞的。
         * 当线程由于特定原因暂停执行，那么它就是阻塞的。
         * 阻塞的线程会立刻交出它的处理器时间片，并从此开始不在小号处理器时间，知道阻塞结束。
         * 
         * WaitSleepJoin四个有用的值:Running、Unstarted、WaitSleepJoin、Stopped
         * 
         * 当线程被阻塞或者解除阻塞时，操作系统就会进行上下文切换。这会导致细小的开销大致在1-2毫秒之间。
         */


        /*
         *I/O密集和计算密集
         * 如果一个操作的绝大部分时间都在等待时间的发生，则称之为I/O密集，例如下载网页或者调用Console.WtriteLine
         * I/O密集操作一般涉及输入或者输出，但是这并非硬性要求。。例如Thread.Sleep也是一种I/O密集操作。
         * 计算密集则是操作的大部分时间都用于执行大量的CPU操作。
         */

        /*
         *阻塞与自旋
         * I/O密集操作主要表现在以下两种形势：要么在当前线程同步进行等待，知道操作完成（例如Console.WriteLine、Sleep、Join）
         * 要么异步进行操作，在操作完成的时候或者之后某个时刻触发回调函数。
         * 
         * 自旋与阻塞有一些细微的差别。首先，非常短暂的自旋在条件可以很快得到满足的场景（例如几毫秒）
         * 下是非常高效的，因为它避免了上下文切换带来的延迟和开销。
         * 阻塞并非没有开销，每一个线程在存货时会占用1mb的内存，并对CLR和操作系统带来持续性 的管理开销。
         * 因此阻塞可能会给繁重的I/O密集型程序带来麻烦，因此这些程序更适合使用回调的方式。
         */
        static void WriteY()
        {

            Thread.Yield();
            Console.WriteLine(Thread.CurrentThread.ThreadState);
            for (int i = 0; i < 100000; i++)
            {
                Console.Write("Y");
            }
        }

        static bool _done;

        static readonly object _lock = new object();
        public static void 本地状态与共享状态()
        {
            //try
            //{
            //    new Thread(Go).Start();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            new Thread(Go).Start();
            Go();
        }
        static void Go()
        {
            lock (_lock)//在不加锁的情况下，这段代码会输出两次Done
            {
                if (!_done) { Console.WriteLine("Done"); _done = true; };//
            }
        }




    }
}
