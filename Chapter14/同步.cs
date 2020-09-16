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

        public static void Invoker()
        {
            // Assign();
            //// Console.WriteLine("what x"+t);
            // Increment();
            // Console.WriteLine(t);
            //syncSet();
            DieLock();
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
        /*
         *选择同步对象
         *若一个对象在各个参与线程中都是可见的，那么该对象就可以作为同步对象。但是该对象必须是一个引用类型队形（这是必须满足 的条件）
         *同步对象通常是私有的（便于封装），而且一般是实例字段或静态字段。同步对象本身也可以是被保护的对象。
         *
         *使用锁的时机
         *若需要访问可写的共享字段，则需要在其周围加锁。
         *如下例子：
         *如果不适用锁则可能出现两个问题：
         *诸如变量自增这类操作并不是原子操作，甚至变量的读写，在某些情况下也不是原子操作。
         *为了提高性能，编译器、CLR乃至处理器都会调整指令的执行顺序并在cpu的寄存器中缓存变量值。只要这种优化不会影响单线程程序（或者使用锁的多线程程序的）的行为即可。
         *
         *使用锁可以避免第二个问题。因为锁会在其前后创建内存栅障，内存栅障就像这些操作的围栏，而指令执行顺序的重排和变量缓存是无法跨越这个围栏的。
         */
        #region 
        static int t;
        public static void Increment()
        {
            lock (_locker)
            {
                t++;
            }

            //t++;
        }
        public static void Assign()
        {
            lock (_locker)
            {
                t = 123;
            }

            //t = 123;
        }

        public static void syncSet()
        {
            var signal = new ManualResetEvent(false);
            int x = 0;
            new Thread(() => { x++; signal.Set(); }).Start();
            signal.WaitOne();
            Console.WriteLine(x);
        }

        #endregion

        #region 嵌套锁
        /*
         * 线程可以嵌套的方式锁住同一个对象。
         */
        public static void NestedLock()
        {

            lock (_locker)
            {
                Assign();
            }
        }
        #endregion
        #region 死锁

        /*
         *  两个线程相互等待对方占用的资源就会使双方都无法执行，从而形成死锁。
         */
        static object locker1 = new object();
        static object locker2 = new object();
        public static void DieLock()
        {
            new Thread(() =>
            {

                lock (locker1)
                {
                    Thread.Sleep(1000);
                    lock (locker2)
                    {

                    }
                }
            }).Start();


            lock (locker2)
            {
                Thread.Sleep(1000);
                lock (locker1)
                {

                }
            }
            Console.WriteLine("xxxxxxxxxxxxxx");
        }
        #endregion
    }
}
