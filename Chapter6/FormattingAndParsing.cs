using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    public class FormattingAndParsing
    {
        public static void Invoker()
        {
            /*格式化是将一个对象转化为字符串，解析则是将字符串转为对象。
             在.netFramework中提供了一系列机制来处理这些操作。
             1.ToString和Parse：这两个方法是很多类型默认具有的功能
             2.格式提供器：对象上其他的ToString（以及Parse）重载方法会接受格式字符串或格式提供器。格式提供器不仅灵活而且与文化相关.Net Framework自带了数字类型和Datetime /Dattimeoffset的格式提供器
             3.XmlConvert：这个静态类提供了基于Xml标准的格式化和解析方法。如果使用文化无关的转换，或希望避免误解析时，可以使用XmlConvert作为有效的通过转换器。XmlConvert支持数字类型、bool、datetime、Dattimeoffset、TimeSpan、Guid。
             4.类转换器（Type converter）：这种转换器时面向设计器XAML解析器的。
             */
            {
                //ToString和Parse
                //ToString会为所有简单值类型产生有意义的字符输出
                //这些值类型都定义了静态的Parse方法来完成反向的转换
                string s = "1";
                Console.WriteLine(s);
                Console.WriteLine("解析为int类型");
                int a = int.Parse(s);
                Console.WriteLine(a);
                //如果解析S失败那么系统会抛出异常（FormatException）,如果使用TryParse那么系统不会抛出异常，而时返回false
                bool b = bool.TryParse(s, out bool isOk);
                Console.WriteLine("解析为bool类型" + isOk);

                //double germany= double.Parse("1.234", CultureInfo.InvariantCulture);
                // Console.WriteLine(germany);
            }
            {
                //格式化提供器：使用格式提供器的方式是IFormatProvider接口，所有的数字类型和Datetime类型都实现了这个接口(所有的enum都可以格式化但是它们没有特殊的IFormatter类)
                //.Net Framework提供了三种格式提供器NumberFormatInfo,DateTimeFormatInfo,CultureInfo

                //在格式提供器的上下文中，CultureInfo扮演了根据文化的区域设置返回NumberFormatInfo和DateTimeFormatInfo两个格式提供器的间接机制。
                //使用适用于en-GB文化默认的NumberFormatInfo对象
                Console.WriteLine("使用特殊文化-英国英语");
                CultureInfo uk = CultureInfo.GetCultureInfo("en-GB");
                Console.WriteLine(3.ToString("C", uk));
                //使用不变的文化对DateTime进行格式化
                DateTime dt = new DateTime(2020, 1, 1);
                CultureInfo iv = CultureInfo.InvariantCulture;
                Console.WriteLine(dt.ToString(iv));
                Console.WriteLine(dt.ToString("d", iv));


                //使用NumberFormatInfo和DateTimeFormatInfo
                //实例化一个NumberFormatInfo对象并且将组分割从逗号修改为空格，并且它将数字格式化为保留小数点后三位的形式。
                NumberFormatInfo f = new NumberFormatInfo();
                f.NumberGroupSeparator = " ";
                Console.WriteLine(12345.6789.ToString("N3", f));


                //组合格式化
                {
                    var s = string.Format(CultureInfo.InvariantCulture, "{0}", "测试");
                    Console.WriteLine(s);
                }

                {
                    object obj = "测试";
                    string s;
                    if (obj is IFormattable)
                    {
                        s = ((IFormattable)obj).ToString(null, CultureInfo.InvariantCulture);
                    }
                    else if (obj == null)
                    {
                        s = "";
                    }
                    else
                    {
                        s = obj.ToString();
                    }
                    Console.WriteLine(s);
                }

                {
                    //通过格式提供器进行解析
                    /*格式提供器并未对解析提供标准结构。相反，每一个参与的类型都会重载它的静态Parse和TryParse方法接受一个格式提供器，以及一个可选的NumberStyles或DateTimeStyles枚举参数
                     NumberStyles和DateTimeStyles的工作方式 ：它们提供了一些自定义的设置。
                     */
                    //例如允许括号或者货币符号出现在输入字符串中（默认这两个选项都是false）
                    //int minusTwo = int.Parse("(2)", NumberStyles.Integer | NumberStyles.AllowParentheses);
                    //Console.WriteLine(minusTwo);
                    ////decimal point = decimal.Parse("  5.20", NumberStyles.Currency, CultureInfo.GetCultureInfo("en-GB"));
                    //decimal point = decimal.Parse("  5.20", NumberStyles.Currency);
                    //Console.WriteLine(point);
                }
                {
                    //IFormatProvider和ICustomFormatter
                    /*所有的格式提供器都是实现了IFormatProvider。这个方法提供了一种间接进行格式化的手段CultureInfo就是用它来返回何时的NumberStyles和DateTimeStyles对象并完成格式化操作的
                     实现IFormatProvider和ICustomFormatter，就能编写自定义的格式提供器（该自定义格式提供器可以和现有的配合工作）
                     */

                    //IFormatProvider fp = new WordFormatProvider();
                    //Console.WriteLine(string.Format(fp, "{0:C} in words is{0:W}", -123.45));
                }

                {
                    //数字格式字符串
                    //Console.WriteLine("数字格式字符串");
                    //Console.WriteLine("====================G或g====================");
                    //Console.WriteLine(string.Format("{0:G}", 1.2345)); 
                    //Console.WriteLine(string.Format("{0:G}", 0.00001)); 
                    //Console.WriteLine(string.Format("{0:g}", 0.00001)); 
                    //Console.WriteLine(string.Format("{0:G3}", 1.2345)); 
                    //Console.WriteLine(string.Format("{0:G}", 12345));
                    //Console.WriteLine("====================F====================");
                    //Console.WriteLine(string.Format("{0:F2}",2345.678));
                    //Console.WriteLine(string.Format("{0:F2}",2345.6));
                    //Console.WriteLine("====================N====================");
                    //Console.WriteLine(string.Format("{0:N2}",2345.678));
                    //Console.WriteLine(string.Format("{0:N2}",2345.6));
                    //Console.WriteLine("====================D====================");
                    //Console.WriteLine(string.Format("{0:D5}",123));
                    //Console.WriteLine(string.Format("{0:D1}",123));
                    //Console.WriteLine("====================E或e====================");
                    //Console.WriteLine(string.Format("{0:E}",56789));
                    //Console.WriteLine(string.Format("{0:e}", 56789));
                    //Console.WriteLine(string.Format("{0:E2}", 56789));
                    //Console.WriteLine("====================C====================");
                    //Console.WriteLine(string.Format("{0:C}",1.2));
                    //Console.WriteLine(string.Format("{0:C4}",1.2));
                    //Console.WriteLine("====================P====================");
                    //Console.WriteLine(string.Format("{0:P}",0.503));
                    //Console.WriteLine(string.Format("{0:P0}",.503));
                    //Console.WriteLine("====================X或x====================");
                    //Console.WriteLine(string.Format("{0:X}",47));
                    //Console.WriteLine(string.Format("{0:f}",47));
                    //Console.WriteLine(string.Format("{0:X4}",47));
                    //Console.WriteLine("====================R或G17====================");
                    //Console.WriteLine(string.Format("{0:R}",1f/3f));
                    //Console.WriteLine(string.Format("{0:G17}",1f/3f));

                }
                {
                    //NumberStyles
                    //Console.WriteLine("====================#====================");
                    //Console.WriteLine(string.Format("{0:.##}",12.345));
                    //Console.WriteLine(string.Format("{0:.####}",12.345));
                    //Console.WriteLine("====================0====================");
                    //Console.WriteLine(string.Format("{0:.00}", 12.345));
                    //Console.WriteLine(string.Format("{0:.0000}", 12.345));
                    //Console.WriteLine(string.Format("{0:000.00}", 99));
                    //Console.WriteLine("====================小数点====================");
                    //Console.WriteLine("====================组分隔符====================");
                    //Console.WriteLine(string.Format("{0:#,###,###}", 1234));
                    //Console.WriteLine(string.Format("{0:0,000,000}", 1234));
                    //Console.WriteLine("====================倍增符号====================");
                    //Console.WriteLine(string.Format("{0:#,}", 1000000));
                    //Console.WriteLine(string.Format("{0:#,,}", 1000000));
                    //Console.WriteLine("====================指数表示法====================");
                    //Console.WriteLine(string.Format("{0:0E0}", 1234));
                    //Console.WriteLine(string.Format("{0:0E+0}", 1234));
                    //Console.WriteLine(string.Format("{0:0.00E00}", 1234));
                    //Console.WriteLine(string.Format("{0:0.00e00}", 1234));
                    //Console.WriteLine("====================转义符====================");
                    //Console.WriteLine(string.Format(@"{0:\#0}", 50));
                    //Console.WriteLine("====================字面量字符引号====================");
                    //Console.WriteLine(string.Format("{0:0'...'}", 50));
                    //Console.WriteLine("====================分段符====================");
                    //Console.WriteLine(string.Format("{0:#;(#);zero}", 15));
                    //Console.WriteLine(string.Format("{0:#;(#);zero}", -5));
                    //Console.WriteLine(string.Format("{0:#;(#);zero}", 0));
                }
                {
                    //Datetime
                    //演示第二种方法
                    //string s = DateTime.Now.ToString("o");
                    //Console.WriteLine(s);
                    //DateTime dt1 = DateTime.ParseExact(s, "o", null);
                    //Console.WriteLine(dt1);
                    //DateTime dt2 = DateTime.Parse(s);
                    //Console.WriteLine(dt2);
                    ////枚举的格式字符串
                    //Console.WriteLine(System.ConsoleColor.Red.ToString("g"));//G或g
                    //Console.WriteLine(System.ConsoleColor.Red.ToString("f"));//F或f
                    //Console.WriteLine(System.ConsoleColor.Red.ToString("d"));//D或d
                    //Console.WriteLine(System.ConsoleColor.Red.ToString("x"));//X或x
                }
                {
                    //操作数字
                    BigInteger x = 4;
                    Complex complex = 3;
                }
            }
        }

        
    }
    public class WordFormatProvider : IFormatProvider, ICustomFormatter
    {
        static readonly string[] _numberWords= new string[] { "zero","one","two","three","four","five","six","seven", "eight", "nine","ten","minus","point"};
        IFormatProvider _parent;
        public WordFormatProvider() :this(CultureInfo.CurrentCulture) { }

        public WordFormatProvider(IFormatProvider parent)
        {
            _parent = parent;
        }
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null || format != "W") return string.Format(_parent, "{0:" + format + "}", arg);
            StringBuilder builder = new StringBuilder();
            string digitList = string.Format(CultureInfo.InvariantCulture, "{0}", arg);
            foreach (char digit in digitList)
            {
                int i = "0123456789-.".IndexOf(digit);
                if (i == -1) continue;
                if (builder.Length > 0) builder.Append(' ');
                builder.Append(_numberWords[i]);
            }
            return builder.ToString();
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return null;
        }
    }
}
