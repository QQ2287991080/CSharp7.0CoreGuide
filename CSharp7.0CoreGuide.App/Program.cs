using Chapter4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7._0CoreGuide.App
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                //测试多播委托
                //Test.Invoker();
            }

            {
                //测试协变和逆变
                //CovariantAndContravariant.Invoker();
            }
            {
                //Event.Invoker();
            }
            {
                //UnSafeCode.Filter(new int[3, 3]);
                //UnSafeCode.StackallocTest();
            }
            {
                //測試 字符串
                Console.WriteLine("abcde abcde".IndexOf("CD", StringComparison.CurrentCultureIgnoreCase));
                Console.WriteLine("abcd".IndexOfAny(new char[] { 'b' }));
                string.Concat("a","b");

                Console.WriteLine(string.Format("Name={0,-20}", "Zero")) ;
                Console.WriteLine(string.Format("Name={0,-20}", "One")) ;


                Console.WriteLine(string.Compare("A", "A"));
                //encoding支持的编码格式
                //foreach (var item in Encoding.GetEncodings())
                //{
                //    Console.WriteLine(item.Name);
                //}

                //encoding将文本转为字节数组
                byte[] utf8Byte = Encoding.UTF8.GetBytes("123456");
                Console.WriteLine(utf8Byte.Length);
                string utf8String = Encoding.UTF8.GetString(utf8Byte);
                Console.WriteLine(utf8String);
            }
            Console.ReadKey();
        }
    }
}
