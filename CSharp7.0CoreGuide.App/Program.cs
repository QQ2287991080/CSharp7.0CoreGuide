using Chapter4;
using Chapter6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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
                XiangDengBiJiao.Invoker();
            }
            Console.ReadKey();
        }
    }
}
