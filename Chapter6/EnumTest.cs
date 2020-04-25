using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Chapter6
{
    public class EnumTest
    {
        [Flags]
        public enum BorderSides
        {
            [Description("左")]
            Left = 1,
            [Description("右")]
            Right = 2,
            [Description("上")]
            Top = 4,
            //[Description("下")]
            Bootom = 8
        }
        public static void Invoker()
        {
            //枚举转整数类型
            A(BorderSides.Bootom);
            B(BorderSides.Bootom);
            C(BorderSides.Bootom);

            //整数转枚举
            //var bs = Enum.ToObject(typeof(BorderSides),5);
            var bs = (BorderSides)3;
            Console.WriteLine(bs);
            //字符串转换
            /*要将一个枚举转换成字符串，可以调用静态的Enum.Format方法或调用实例的ToString方法。每一个方法都会接受格式字符串字符串参数。其中G标识默认的格式化行为，D标识将实际的整数值输出为字符串X和D一样*/
            var leftenum= Enum.Format(typeof(BorderSides),BorderSides.Left,"G");
            Console.WriteLine(leftenum);
            var leftright = (BorderSides)Enum.Parse(typeof(BorderSides), "Left");
            Console.WriteLine(leftright);
            //列举枚举值
            foreach (var item in Enum.GetValues(typeof(BorderSides)))
            {
                Console.WriteLine(item);
              
            }
            foreach (var item in Enum.GetNames(typeof(BorderSides)))
            {
                Console.WriteLine(item);
            }
            //枚举的语义很大程度上都是由编译器决定的。在CLR中，枚举实例（未拆箱）与它的实际整数值在运行是没有区别。而且，CLR中定义的枚举仅仅是System.Enum的子类型，而每一个成员则是其静态整数类型字段。这意味着在通常情况下使用枚举是非常高效的，其运行开销和整数常量的开销一致。
            GetEnumDescription(BorderSides.Bootom);
            Console.WriteLine(GetEnumDescription(BorderSides.Bootom));
            Console.WriteLine("=======================");
            var result= BorderSides.Left + 1;
            var result2= BorderSides.Left;
            Console.WriteLine(result2);
            Console.WriteLine(result);
        }

        static string GetEnumDescription(Enum anyEnum)
        {
            var type = anyEnum.GetType();//获取枚举的类型
            var name = Enum.GetName(type, anyEnum);//获取枚举值的名字
            if (name == null) return null;
            var filed = type.GetField(name);//查看枚举类型中是否有这个枚举值
            var attribute = Attribute.GetCustomAttribute(filed, typeof(DescriptionAttribute)) as DescriptionAttribute;//获取备注特性
            if (attribute == null) return name;
            return attribute?.Description;
        }
        /// <summary>
        /// 适应任意长度
        /// </summary>
        /// <param name="anyEnum"></param>
        /// <returns></returns>
        static decimal A(Enum anyEnum)
        {
            return Convert.ToDecimal(anyEnum);
        }
        /// <summary>
        /// 使用Enum.GetUnderlyingType方法获取枚举的整数类型
        /// </summary>
        /// <param name="anyEnum"></param>
        /// <returns></returns>
        static object B(Enum anyEnum)
        {
            var type = Enum.GetUnderlyingType(anyEnum.GetType());
            return Convert.ChangeType(anyEnum, type);
        }
        /// <summary>
        /// 枚举字符格式化
        /// </summary>
        /// <param name="anyEnum"></param>
        /// <returns></returns>
        static string C(Enum anyEnum)
        {
            return anyEnum.ToString("d");
        }

        static int GetValue(Enum anyEnum)
        {
            return (int)(object)anyEnum;
        }
    }

    
}
