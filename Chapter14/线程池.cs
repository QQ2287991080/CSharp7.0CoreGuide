using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14
{
    public class 线程池
    {
        public static void Invoker()
        {
            /*
             *每当启动一个线程时，都需要一定的时间（几百毫秒）来创建新的局部变量栈。
             * 而线程池通过预先创建一个回收线程的池子来降低这个开销。
             * 线程池对开发高性能的并行程序和细粒度的并发都是非常重要的。
             * 它可以支持运行一些短暂的操作而不会受到线程启动开销的影响。
             * 1.线程池中线程的Name属性是无法就行设置的
             * 2.线程池中的线程都是后台程序
             * 3.阻塞线程池中的线程会影响性能。
             * 
             * 
             * 我们先可以设置线程池中任意线程的优先级，当线程返回给线程池时其优先级恢复为普通级别。
             * 
             */

            var isPool= Thread.CurrentThread.IsThreadPoolThread;//确认当前运行的线程是否是一个线程池线程。
            Console.WriteLine(isPool);
            //进入线程池
            Task.Run(() => Console.WriteLine("进入线程池"));
            //小于.net4.0
            ThreadPool.QueueUserWorkItem(not => Console.WriteLine(".net 3.5进入线程池"));
        }
    }
}
