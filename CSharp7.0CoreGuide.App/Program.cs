using Chapter4;
using System;
using System.Collections.Generic;
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
                CovariantAndContravariant.Invoker();
            }
            {
                Event.Invoker();
            }
            Console.ReadKey();
        }
    }
}
