using System;
using System.Collections.Generic;
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
            { 
            
            }

        }
    }
}
