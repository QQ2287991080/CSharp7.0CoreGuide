using Chapter14;
using Chapter4;
using Chapter6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Environment;

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
                //Console.WriteLine("abcde abcde".IndexOf("CD", StringComparison.CurrentCultureIgnoreCase));
                //Console.WriteLine("abcd".IndexOfAny(new char[] { 'b' }));
                //string.Concat("a","b");

                //Console.WriteLine(string.Format("Name={0,-20}", "Zero")) ;
                //Console.WriteLine(string.Format("Name={0,-20}", "One")) ;


                //Console.WriteLine(string.Compare("A", "A"));
                //encoding支持的编码格式
                //foreach (var item in Encoding.GetEncodings())
                //{
                //    Console.WriteLine(item.Name);
                //}

                //encoding将文本转为字节数组
                //byte[] utf8Byte = Encoding.UTF8.GetBytes("123456");
                //Console.WriteLine(utf8Byte.Length);
                //string utf8String = Encoding.UTF8.GetString(utf8Byte);
                //Console.WriteLine(utf8String);
            }
            {
                //时间
                //TimeSpan time = new TimeSpan(2,2,2);
                //Console.WriteLine(time);
                //var time2 =TimeSpan.FromDays(2);
                //Console.WriteLine(time2);
                //Console.WriteLine(DateTime.Now.TimeOfDay);
                //Console.WriteLine(TimeSpan.Zero);
                ////使用datetimeKind
                //Console.WriteLine(new DateTime(5767,DateTimeKind.Local));
                //var calender = new HebrewCalendar();
                ////最大支持时间
                //Console.WriteLine(calender.MaxSupportedDateTime);
                ////最小支持时间
                //Console.WriteLine(calender.MinSupportedDateTime);
                //获取月份
                //Console.WriteLine(calender.GetMonth(DateTime.Now));
                //DateTime time3 = new DateTime(5767, 1, 1, new System.Globalization.HebrewCalendar());
                //Console.WriteLine(time3);
                //var t1 = DateTime.Now;
                //var t2 = t1.AddDays(2);
                //TimeSpan ts = t2 - t1;
                //Console.WriteLine(ts.Ticks);
                //Console.WriteLine(t1.ToString("o"));//指定与文化无关的格式化字符串
                //Console.WriteLine(DateTime.Parse(t1.ToString("o")));

                //FormattingAndParsing.Invoker();

            }
            {
                ////类型转换器
                //TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));
                //Color beige = (Color)converter.ConvertFromString("Beige");//Color[Beige]
                //Color purple = (Color)converter.ConvertFromString("#800080");//Color[Purple]
                //Color window = (Color)converter.ConvertFromString("Window");//Color[Window]

                //{
                //    decimal a = 10;
                //    var b = decimal.GetBits(a);
                //    decimal c = new Decimal(b);
                //    Console.WriteLine(a == c);//true
                //}

                //{
                //    var a= DateTime.Now.ToBinary();
                //    var b = BitConverter.GetBytes(a);
                //    var d= new DateTime(a);
                //    var c= DateTime.FromBinary(a);
                //}
            }
            {
                //操作数字
                //OperateNumber.Invoker();
                //EnumTest.Invoker();
                //XiangDengBiJiao.Invoker();
                //顺序比较.Invoker();
                //Console.WriteLine("根目录"+System.Environment.CurrentDirectory);
                //Console.WriteLine("系统目录"+System.Environment.SystemDirectory);
                //Console.WriteLine("该进程的命令行"+System.Environment.CommandLine);
                //Console.WriteLine("获取此本地计算机的 NetBIOS 名称。" + System.Environment.MachineName);
                //Console.WriteLine("获取当前计算机上的处理器数" + System.Environment.ProcessorCount);
                //Console.WriteLine("一个包含平台标识符和版本号的对象" + System.Environment.OSVersion);
                //Console.WriteLine("获取为此环境定义的换行字符串。" + System.Environment.NewLine);
                //Console.WriteLine("获取当前登录用户"+System.Environment.UserName);
                //Console.WriteLine("获取一个值，用以指示当前进程是否在用户交互模式中运行" + System.Environment.UserInteractive);
                //Console.WriteLine("获取与当前用户关联的网络域名。" + System.Environment.UserDomainName);
                //Console.WriteLine("获取系统启动后经过的毫秒数" + System.Environment.TickCount);
                //Console.WriteLine("获取当前的堆栈跟踪信息。" + System.Environment.StackTrace);
                //Console.WriteLine(" 获取映射到进程上下文的物理内存量。" + System.Environment.WorkingSet);
                //Console.WriteLine("用于显示公共语言运行时版本的对象" + System.Environment.Version);
                //Console.WriteLine("从当前进程检索环境变量的值" + System.Environment.GetEnvironmentVariable("Path"));
                //Console.WriteLine("从当前进程检索环境变量的值" + System.Environment.GetEnvironmentVariables());
                //Console.WriteLine("从当前进程或者从当前用户或本地计算机的 Windows 操作系统注册表项检索所有环境变量名及其值" + System.Environment.GetEnvironmentVariables());
                //Console.WriteLine("返回桌面的路径" + System.Environment.GetFolderPath(SpecialFolder.Desktop));
                //AppContext.SetSwitch(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\腾讯软件\QQ\腾讯QQ.exe", true);
                //Console.WriteLine("A".CompareTo("B"));//-1
                //Console.WriteLine("B".CompareTo("B"));//0
                //Console.WriteLine("B".CompareTo("A"));//1
            }
            {
                //ProcessStartInfo psi = new ProcessStartInfo
                //{
                //    FileName = @"D:\tim\Bin\QQScLauncher.exe",
                //    //Arguments = "/c ipconfig /all",
                //    RedirectStandardOutput = true,
                //    UseShellExecute = false
                //};
                //Process process = Process.Start(psi);
                //string result = process.StandardOutput.ReadToEnd();
                //Console.WriteLine(result);
                
            }
            {
           
                //线程.Invoker();
                汇合和休眠.本地状态与共享状态();
            }
            Console.ReadKey();
        }
    }
}
