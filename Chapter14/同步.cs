using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 同步
    {
        /*
         * 排它锁：排它锁每一次只允许一个线程执行特定的活动或一段代码。它的主要目的是令线程访问共享的写状态而不互相影响。排它锁包括lock、mutex、spinlock
         * 非排它锁：非排它锁实现了有限的并发性。非排它锁包括Semaphore(Slim)和ReaderWriterLock(Slim).
         * 信号发送结构：这种结构允许线程在接到一个或多个其他线程的通知之前保持阻塞状态。信号发送结构包括ManualResetEvent(Slim)、AutoResetEvent(Slim)、CountdownEvent和Barrier。前三者是就是所谓的时间等待句柄（event wait handles）。
         * 
         * 一些结构在不适用锁的前提下也可以处理特定的共享状态的同步操作，称之为非阻塞同步结构（nonblocking synchronization constructs）
         * 它们包括Thread.MemoryBarrier、 Thread.VolatileWrite、volatile关键字和Interlocked类
         * 
         */

        public  static void Invoker()
        {
            for (int i = 0; i < 1000; i++)
            {
                LockTakenTest();
            }
        }
        #region lock语句
        static readonly object _locker = new object();
        static int x = 1, y = 2;
        public static void testA()
        {

            lock (_locker)
            {
                if (x != 0) Console.WriteLine(y /= x);
                x = 0;
            }
        }
        #endregion

        #region Monitor.Enter and Monitor.Exi
        /*
         * Monitor--监控
         * 事实上C#的lock关键字是包裹在try/finally语句块中的Monitor.Enter和 Monitor.Exit语法糖。
         */
        public static void overrideTestA()
        {
            Monitor.Enter(_locker);
            try
            {
                if (x != 0) Console.WriteLine(y /= x);
                x = 0;
                var b = x / 1;
            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }

        /*
         * 上述代码在有一个漏洞，如果在Monitor.Enter和try语句之间抛出了异常
         * 那么锁的状态是不确认的，如果已经获得了锁，那么这个锁将永远无法释放
         * 因为我们没有机会进入finally释放代码块。
         * 所以这种情况会造成锁泄露。
         * 针对这种情况C#4.0 已经CLR4进行了重新设计
         */

        public static void LockTakenTest()
        {

            // 升级overridetestA
            bool lockTaken = false;
           
            try
            {
                //Monitor.Enter(_locker,ref lockTaken);
               var isenter= Monitor.TryEnter(_locker, TimeSpan.FromMilliseconds(0.000000000000000000000000000000000000000000000000000000000000001));
                if (isenter)
                {
                    Console.WriteLine("get locked");
                }
                if (x != 0) Console.WriteLine(y /= x);
                x = 0;
               
            }
            finally
            {

                //if (lockTaken) {
                //    Monitor.Exit(_locker);
                //}

                if (lockTaken)
                {
                    Monitor.Exit(_locker);
                }
            }
        }
        #endregion
    }
}
