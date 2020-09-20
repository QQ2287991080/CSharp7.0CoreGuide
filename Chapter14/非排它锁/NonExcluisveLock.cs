using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter14.非排它锁
{
    /// <summary>
    /// 非排它🔒
    /// </summary>
    public class NonExcluisveLock
    {
        /*
         *信号量就像俱乐部一样：它有特定的容量，还有门卫保护。一旦满员之后就不允许其他人进入了，人们只能在外面排队，当有人离开时，才准许另外一个人进入。
         */

        public static void Invoker()
        {

        }
    }
    #region Semaphore
    //信号量可以用于限制并发性，阻止太多线程同时执行特定的代码
    public class TheClub
    {

        static SemaphoreSlim _sem = new SemaphoreSlim(3);// just  3 people

        public static void Main()
        {
            for (int i = 0; i < 5; i++)
            {
                new Thread(Enter).Start(i);
            }
        }
        static void Enter(object id)
        {
            Console.WriteLine(id + " want to enter");
            _sem.Wait();
            Console.WriteLine(id + " is in");
            Thread.Sleep(1000 * (int)id);
            Console.WriteLine(id + " is leaving");
            _sem.Release();
        }
    }
    #endregion


    #region 读写锁
    /*
     *通常一个类型实例的并发读操作是线程安全的，而并发更新操作则不是。诸如文件这样的资源也具有相同的特点。虽然可以简单的使用一个排它锁来保护对实例的任何形式的访问。
     *但是如果其读操作很多但是更新操作很少，则使用单一的锁限制并发性就不大合理了。
     *这种情况出现在业务应用服务器上，它会将常用的数据缓存在静态字段中进行快速检索。
     *ReaderWriteLockSlim是专门为这种情形设计的，它可以最大限度的保证锁的可用性。
     *
     *ReaderWriteLockSlim在.net3.5引入的它替代了笨重的ReaderWriteLock类。虽然两者功能相识，但是后者的执行速度比前置慢数倍。
     *
     *ReaderWriteLockSlim和ReaderWriterLock都拥有两种基本锁，读和写。
     *写锁是全局排它锁
     *读锁可以兼容其他的锁
     *
     *因此，一个持有写锁的线程将阻塞其他任何试图获取读锁或写锁的京城。但是如果没有任何线程持有写锁的话，那么任意数量的线程都可以获得读锁。
     *ReaderWriterLockSlim和之前的lock一样也有类似TryEnter之类的方法，来判断是否超时，如果超时就抛出错误（lock返回false）
     */
    #endregion
}
