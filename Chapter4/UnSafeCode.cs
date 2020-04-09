using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    /// <summary>
    /// 不安全代码
    /// </summary>
    public class UnSafeCode
    {
       public static unsafe void Filter(int[,] map)
        {
            int length = map.Length;
            fixed (int* b = map)
            {
                int* p = b;
                for (int i = 0; i < length; i++)
                {
                    *p++ &= 0xFF;
                    Console.WriteLine(*p++ &= 0xFF);
                }
            }
        }
        /// <summary>
        /// Stackalloc显式在栈上分配内存
        /// </summary>
        public static void StackallocTest()
        {
            unsafe
            {
                int* a = stackalloc int[10];
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(a[i]);
                }
            }
        }
    }
}
