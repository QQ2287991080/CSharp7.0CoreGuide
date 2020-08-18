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
                testA();
            }
        }

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
         
    }
}
