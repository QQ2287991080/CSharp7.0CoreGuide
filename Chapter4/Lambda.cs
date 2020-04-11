using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    /// <summary>
    /// 测试lamdba
    /// </summary>
    public class Lambda
    {
        /*Lamdba表达式是一种可以替代委托实例的匿名方法
        编译器会讲lamdba表达式转化为两种形式：
        1.一个委托实例
        2.一个类型为Expression<TDelegate>的表达式树，该表达式内部的代码表现为一个可遍历的对象模型
        ---捕获外部变量
        Lamdba表达式所引用的外部变量称为捕获变量，捕获变量的表达式成为闭包。（捕获的变量会在正在调用委托的时候赋值，而不是在捕获时赋值）


                     */
        public static void Invoker()
        {
            {
                //捕获外部变量


                //int init = 1;
                //Func<int, int> fun = f => f * init;
                //init = 2;
                //Console.WriteLine(fun.Invoke(3));

            }
            {
                //捕获迭代变量


                try
                {
                    Action[] actions = new Action[4];
                    for (int i = 0; i < 4; i++)
                    {
                        actions[i] = () => Console.WriteLine(i);
                    }

                    foreach (var item in actions)
                    {
                        item();
                    }
                }
                catch (WebException ex) when (ex.Status==WebExceptionStatus.Timeout)
                {
                    throw;
                }
            }
            Console.WriteLine("==========================");
            {
                /*C# 4.0之前foreach和for的效果是一样的，C#5.0修复了*/
                int i = 0;
                Action[] actions = new Action[3];
                foreach (char item in "abc")
                {
                    actions[i++] = () => Console.WriteLine(item);
                }

                foreach (var item in actions)
                {
                    item();
                }
            }
            {
                //斐波那契数列
                foreach (var item in Fibs(6))
                {
                    Console.WriteLine(item);
                }


            }
        }
        static IEnumerable<int> Fibs(int count)
        {
            for (int i = 0,prevFib=1,curFib=1; i < count; i++)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }
    }
}
