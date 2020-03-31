using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chapter4.Delegates;

namespace Chapter4
{
    public class Delegates
    {
        public delegate void ProgressReporter(int a);
        public class Util
        {
            public static void HardWork(ProgressReporter reporter)
            {
                for (int i = 0; i < 10; i++)
                {
                    reporter(i * 10);
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
    /// <summary>
    /// 多播委托示例
    /// <remake>
    /// 所有的委托都有多播能力+,+=可以按顺序添加委托，-,-=会从左侧委托操作数中删除右侧委托
    /// 如果委托的返回参数是int类型，那么多播委托返回最后一个委托的返回参数，其他的返回参数会在传播过程中丢弃。
    /// 如果是void就不会有这种情况
    /// tips:所有的委托都隐式继承System.MulticastDelegate而System.MulticastDelegate继承自System.Delegate,C#将委托中的+，-,+=,-+都编译成System.Delegate中的静态方法，Combine喝Remove
    /// </remake>
    /// </summary>
    public static class Test
    {
        public static void Invoker()
        {
            ProgressReporter method = WriteToConsole;
            method += WriteToFile;
            Util.HardWork(method);
        }

        static void WriteToConsole(int a) => Console.WriteLine(a);
        static void WriteToFile(int a) => Console.WriteLine("创建文件：{0}", a.ToString());
    }
}
