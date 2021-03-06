﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    public class XiangDengBiJiao
    {
        /*
         相等的比较：
         值相等和引用相等。
         值相等：两个值在某种意义上是想等的。
         引用相等：两个引用指向完全相同的对象。
         默认情况下：
         值类型使用值相等。
         引用类型使用引用相等。

        1.标准等值比较协议

                 */

        public static void Invoker()
        {
            Console.WriteLine("=========标准等值比较协议===========");
            //1.标准等值比较协议
            /*
             ==和!=
             object对象Equals虚方法
             IEquatble<T>接口
             */
            Console.WriteLine("等于和不等于");
            //很多的例子中都使用了标准的==和！=运算符进行相等/不相等的比较。而==与！=的特殊性在于它们的运算符，因此它们是静态解析的（实际上，它们的本身就是静态函数）。因此使用==和！=时，c#会在编译时根据类型确认时哪一个函数执行比较操作，且这里没有任何虚行为。
            {
                int x = 5;
                int y = 5;
                Console.WriteLine(x==y);//true
                //这个例子编译器会把==绑定在int类型上，int是值类型，所以此时==进行的是值类型比较，所有为true
            }
            {
                object x = 5;
                object y = 5;
                Console.WriteLine(x == y);//false
               //这个例子编译器会把==绑定在object类型上，object是引用类型，所以此时==进行的是引用比较，所有为false
            }
            Console.WriteLine("Object对象Equals虚方法");
            //Equals定义在System.Object上的方法，所以所有的类型都支持这个方法。
            //Equals在运行时根据对象的实例类型解析的。如果你这个是对象是int类型的那么它实际调用的是int的Equals方法。对于引用类型Equals默认进行引用相等比较，而对于结构体，Equals会对结构体的每一个字段的Equals进行结构化比较。
            {
                object x = 5;
                object y = 5;
                Console.WriteLine(x.Equals(y));//false
            }
            Console.WriteLine("object.Equals静态方法");
            //object.Equals静态方法。
            //object 的类提供一个静态的辅助方法，该方法正是实现上一个例子中的AreEqual操作。虽然它们的名字与虚方法相同，但是它们不会冲突。
            //这个方法可以提供null值的相等比较
            {
                object x = 1, y = 2;
                object.Equals(x, y);
                x = null;
                object.Equals(x, y);
                y = null;
                object.Equals(x, y);

                //泛型的使用
            }
            {
                Console.WriteLine("object.ReferenceEquals");
                //object.ReferenceEquals
                StringBuilder builder1 = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                Console.WriteLine(object.ReferenceEquals(builder1, builder1));//true
                Console.WriteLine(object.ReferenceEquals(builder1, builder2));//false
            }
            {
                //重写IEquatable<T>
                Console.WriteLine("重写IEquatable<T>");
                var time1 = DateTime.Now;
                Area area = new Area(5,3);
                Area area2 = new Area(3, 5);
                Console.WriteLine(area.Equals(area2));//true
                Console.WriteLine(area == area2);//true
                var time2 = DateTime.Now;
                Debug.WriteLine("结构体："+(time2 - time1).TotalSeconds);
            }
            {
                var time1 = DateTime.Now;
                Area2 area = new Area2(5, 3);
                Area2 area2 = new Area2(3, 5);
                Console.WriteLine(area.Equals(area2));
                Console.WriteLine(area == area2);
                var time2 = DateTime.Now;
                Debug.WriteLine("类："+(time2 - time1).TotalSeconds);
            }
        }

        public class Test<T>
        {
            T _value;
            public void SetValue(T value)
            {
                if (!object.Equals(value,_value))
                {
                    _value = value;
                }
            }
        }
    }
    public class Area2
    {
        public int Measure1 { get; set; }
        public  int Measure2 { get; set; }
        public Area2(int m1, int m2)
        {
            Measure1 = Math.Min(m1, m2);
            Measure2 = Math.Max(m1, m2);
        }
    }
    public struct Area : IEquatable<Area>
    {
        public readonly int Measure1;
        public readonly int Measure2;
        public Area(int m1,int m2)
        {
            Measure1 = Math.Min(m1, m2);
            Measure2 = Math.Max(m1, m2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Area)) return false;
            return Equals((Area)obj);
        }
        /// <summary>
        /// 由Json Bloch推荐的模式
        /// 
        /// </summary>
        /// <remarks>
        /// int hash=17;
        /// hash=hash*31+field1.GetHashCode();
        /// hash=hash*31+field2.GetHashCode();
        /// return hash;
        /// </remarks>
        /// <returns></returns>
        public override int GetHashCode() => Measure1 + Measure2 * 31;
        public override string ToString()
        {
            return (Measure1 + Measure2).ToString();
        }
        public bool Equals(Area other) => Measure1 == other.Measure1 && Measure2 == other.Measure2;

        /// <summary>
        /// 重载运算符
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static bool operator ==(Area a1, Area a2) => a1.Equals(a2);
        public static bool operator !=(Area a1, Area a2) => !a1.Equals(a2);
    }

   
}
