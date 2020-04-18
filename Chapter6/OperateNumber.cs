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
                Type targetType = typeof(int);
                object source = "42";
                object result = Convert.ChangeType(source, targetType);
                Convert.ChangeType(source, targetType);
                Console.WriteLine(result);//42
                Console.WriteLine(result.GetType());//System.Int32
            }
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                Type targetType = typeof(double);
                string source = "1,034,562.91";
                var result = Convert.ChangeType(source, targetType,culture);
                Console.WriteLine(result);//1034562.91
                Console.WriteLine(result.GetType());//System.Double

            }
            
        }
    }
}
