using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 线程
    {

        /*
         *线程是一个可以独立执行的执行路径
         * 每一个线程都运行在一个操作系统进程中。这个进程提供了程序执行的独立环境
         * 在单线程程序中，进程中只有一个线程运行。因此线程可以独立使用进程环境。
         * 而在多线程程序中，一个进程中会运行多个线程。它们共享同一个执行环境（特别是内存。）。
         */

        public static void Invoker()
        {
            //开启一个线程
            new Thread(() =>WriteY()).Start();
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("X");
            }
            Console.WriteLine("主线程名字："+Thread.CurrentThread.Name);
           /*主线程会创建一个新的线程，用来执行方法重复输出y，同时主线程也会重复输出x.
            * 在单核计算机上，操作系统会为每一个线程划分时间片（Windows系统的典型值为20毫秒）
            * 来模拟执行并发。因此上述代码会出现连续的x和y。
            * 而在一个多核心的机器上。两个线程可以并行执行（会和机器上其它执行的程序进行竞争）
            * 因此虽然我们还是会得到连续的x和y，但这是由于Console处理并发请求的机制导致的。
            *
            * 线程是抢占式的。它的执行和其他的线程的代码时交错执行的。这个术语通常可以解释一些由此产生的问题。
            * 线程一旦启动，其IsAlive属性就会返回true，直到线程停止。当Thread构造函数接受的委托执行完毕。线程停止之后旧无法再启动了。
            * 
            * 
            * 
            * 
             */
        }

        static void WriteY()
        {
            Console.WriteLine("副线程名字：" + Thread.CurrentThread.Name);
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("Y");
            }
        }
    }
}
