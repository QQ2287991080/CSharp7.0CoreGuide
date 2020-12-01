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
     *因此，一个持有写锁的线程将阻塞其他任何试图获取读锁或写锁的进程。但是如果没有任何线程持有写锁的话，那么任意数量的线程都可以获得读锁。
     *ReaderWriterLockSlim和之前的lock一样也有类似TryEnter之类的方法，来判断是否超时，如果超时就抛出错误（lock返回false）
     *
     */

    public class SlimDome
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static List<int> _items = new List<int>();
        static Random _rand = new Random();

        public static void Invoker()
        {

            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Write).Start("A");
            new Thread(Write).Start("B");
        }

        static void Read()
        {
            while (true)
            {
                _rw.EnterReadLock();
                foreach (var item in _items)
                {
                    Thread.Sleep(10);
                }
                _rw.ExitReadLock();
            }
        }

        static void Write(object threadId)
        {
            while (true)
            {
                int number = GetRandNun(10);
                _rw.EnterWriteLock();
                _items.Add(number);
                _rw.ExitWriteLock();
                Console.WriteLine($"Thread 【{threadId}】 add 【{number}】");
                Console.WriteLine($"Count:"+_rw.CurrentReadCount);
                Thread.Sleep(100);
            }
        }
        static int GetRandNun(int max)
        {
            lock (_rand)
            {
                return _rand.Next(max);
            }
        }
    }

    public class SynchronizedCache
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private Dictionary<int, string> innerCache = new Dictionary<int, string>();

        public int Count
        { get { return innerCache.Count; } }

        public string Read(int key)
        {
            cacheLock.EnterReadLock();
            try
            {
                return innerCache[key];
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public void Add(int key, string value)
        {
            cacheLock.EnterWriteLock();
            try
            {

                innerCache.Add(key, value);
                Console.WriteLine($"Add--【{value}】");
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public bool AddWithTimeout(int key, string value, int timeout)
        {
            if (cacheLock.TryEnterWriteLock(timeout))
            {
                try
                {
                    innerCache.Add(key, value);
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public AddOrUpdateStatus AddOrUpdate(int key, string value)
        {
            cacheLock.EnterUpgradeableReadLock();
            try
            {
                string result = null;
                if (innerCache.TryGetValue(key, out result))
                {
                    if (result == value)
                    {
                        Console.WriteLine($"AddOrUpdate--已存在】");
                        return AddOrUpdateStatus.Unchanged;
                    }
                    else
                    {
                        cacheLock.EnterWriteLock();
                        try
                        {
                            Console.WriteLine($"AddOrUpdate--更新】");
                            innerCache[key] = value;
                        }
                        finally
                        {
                            cacheLock.ExitWriteLock();
                        }
                        return AddOrUpdateStatus.Updated;
                    }
                }
                else
                {
                    cacheLock.EnterWriteLock();
                    try
                    {
                        Console.WriteLine($"AddOrUpdate--写入】");
                        innerCache.Add(key, value);
                    }
                    finally
                    {
                        cacheLock.ExitWriteLock();
                    }
                    return AddOrUpdateStatus.Added;
                }
            }
            finally
            {
                cacheLock.ExitUpgradeableReadLock();
            }
        }

        public void Delete(int key)
        {
            cacheLock.EnterWriteLock();
            try
            {
                innerCache.Remove(key);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public enum AddOrUpdateStatus
        {
            Added,
            Updated,
            Unchanged
        };

        ~SynchronizedCache()
        {
            if (cacheLock != null) cacheLock.Dispose();
        }
    }


    public class Example
    {
        public static void Main()
        {
            var sc = new SynchronizedCache();
            var tasks = new List<Task>();
            int itemsWritten = 0;

            // Execute a writer.
            tasks.Add(Task.Run(() => {
                String[] vegetables = { "broccoli", "cauliflower",
                                                          "carrot", "sorrel", "baby turnip",
                                                          "beet", "brussel sprout",
                                                          "cabbage", "plantain",
                                                          "spinach", "grape leaves",
                                                          "lime leaves", "corn",
                                                          "radish", "cucumber",
                                                          "raddichio", "lima beans" };
                for (int ctr = 1; ctr <= vegetables.Length; ctr++)
                    sc.Add(ctr, vegetables[ctr - 1]);

                itemsWritten = vegetables.Length;
                Console.WriteLine("Task {0} wrote {1} items\n",
                                  Task.CurrentId, itemsWritten);
            }));
            // Execute two readers, one to read from first to last and the second from last to first.
            for (int ctr = 0; ctr <= 1; ctr++)
            {
                bool desc = Convert.ToBoolean(ctr);
                tasks.Add(Task.Run(() => {
                    int start, last, step;
                    int items;
                    do
                    {
                        String output = String.Empty;
                        items = sc.Count;
                        if (!desc)
                        {
                            start = 1;
                            step = 1;
                            last = items;
                        }
                        else
                        {
                            start = items;
                            step = -1;
                            last = 1;
                        }

                        for (int index = start; desc ? index >= last : index <= last; index += step)
                            output += String.Format("[{0}] ", sc.Read(index));

                        Console.WriteLine("Task {0} read {1} items: {2}\n",
                                          Task.CurrentId, items, output);
                    } while (items < itemsWritten | itemsWritten == 0);
                }));
            }
            // Execute a red/update task.
            tasks.Add(Task.Run(() => {
                Thread.Sleep(100);
                for (int ctr = 1; ctr <= sc.Count; ctr++)
                {
                    String value = sc.Read(ctr);
                    if (value == "cucumber")
                        if (sc.AddOrUpdate(ctr, "green bean") != SynchronizedCache.AddOrUpdateStatus.Unchanged)
                            Console.WriteLine("Changed 'cucumber' to 'green bean'");
                }
            }));

            // Wait for all three tasks to complete.
            Task.WaitAll(tasks.ToArray());

            // Display the final contents of the cache.
            Console.WriteLine();
            Console.WriteLine("Values in synchronized cache: ");
            for (int ctr = 1; ctr <= sc.Count; ctr++)
                Console.WriteLine("   {0}: {1}", ctr, sc.Read(ctr));
        }
    }
    #endregion
}
