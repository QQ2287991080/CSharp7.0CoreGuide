using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    /// <summary>
    /// 操作数字
    /// </summary>
    public class OperateNumber
    {
        public static void Invoker()
        {
            double d = 3.9;
            int i = Convert.ToInt32(d);//i==4


            int thirty = Convert.ToInt32("1E", 16);//30 Parse in hexadecimal
            uint five = Convert.ToUInt32("101", 2);//5 Parse in binary
            Console.WriteLine(thirty);
            Console.WriteLine(five);

            {
                //Math
                Console.WriteLine("==============BigInteger===============");
                //BigInteger:BigInteger结构体是.NetFramework4.0新增的特殊值类型，它位于System.Numerics.dll的System.Nuerics命名空间中。它可以表示任意大的整数而不会丢失精度。
                System.Numerics.BigInteger twentyfive = 25;
                Console.WriteLine(twentyfive);
                System.Numerics.BigInteger googol = System.Numerics.BigInteger.Pow(10, 100);//10的100次方
                Console.WriteLine(googol.ToString());
                //BigInteger重载了包括去余数（%）在内的所有算术运算符，还重载了顺序比较运算符
                //另一种创建Biginteger的方式是从字节进行创建。
                System.Security.Cryptography.RandomNumberGenerator ran = System.Security.Cryptography.RandomNumberGenerator.Create();
                byte[] bytes = new byte[32];
                ran.GetBytes(bytes);
                var bigNumber = new System.Numerics.BigInteger(bytes);
                Console.WriteLine(bigNumber);
                Console.WriteLine("==============Cmoplex===============");
                //Cmoplex结构体也是.NetFramework4.0新增的特殊值类型，它表示实部和虚部均为double类型的负数。
                var c1 = new System.Numerics.Complex(2, 3.5);
                Console.WriteLine(c1.Real);//实部
                Console.WriteLine(c1.Imaginary);//虚部
                Console.WriteLine(c1.Phase);//复数的相位
                Console.WriteLine(c1.Magnitude);//复数的量值
                Console.WriteLine(System.Numerics.Complex.Asin(c1));
                Console.WriteLine("==============Random===============");
                //Random类能够生成类型为byte、integer、或double的伪随机数序列。
                //使用Random之前需要将其序列化，并可以传递一个可选的种子参数来初始化随机数序列。使用相同的种子（在相同的CLR版本下）一定会产生相同序列的数字。这个特性在重现过程中非常有用：
                Random r1 = new Random(1);
                Random r2 = new Random(1);
                Console.WriteLine(r1.Next(100) + "," + r1.Next(100));
                Console.WriteLine(r2.Next(100) + "," + r2.Next(100));
                //若不需要重现性，那么创建Random时无需提供种子，此时将用当前系统时间来生成种子。
                /*由于系统时钟只有有限的粒度，因此两个创建时间非常接近（一般在10毫秒之内的）Random实例会产生相同值的序列。常用的方法是每当需要一个随机数时才序列化一个Random对象，而不是重用一个对象。声明单例的静态Random实例是一个不错的模式，但是在多线程环境下可能出现问题，因此Random对象并非线程安全的。*/
                System.Security.Cryptography.RandomNumberGenerator rand = System.Security.Cryptography.RandomNumberGenerator.Create();
                byte[] bytes2 = new byte[4];
                rand.GetBytes(bytes2);
                var o = BitConverter.ToInt32(bytes2,0);
                Console.WriteLine(o);
            }



            //动态转换
            //{
            //    Type targetType = typeof(int);
            //    object source = "42";
            //    object result = Convert.ChangeType(source, targetType);
            //    Convert.ChangeType(source, targetType);
            //    Console.WriteLine(result);//42
            //    Console.WriteLine(result.GetType());//System.Int32
            //}
            //{
            //    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            //    Type targetType = typeof(double);
            //    string source = "1,034,562.91";
            //    var result = Convert.ChangeType(source, targetType,culture);
            //    Console.WriteLine(result);//1034562.91
            //    Console.WriteLine(result.GetType());//System.Double

            //}

        }
    }
}
