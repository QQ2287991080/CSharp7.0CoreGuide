using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chapter19
{
    public class Fanshe
    {
        public static void Invoker()
        {
            var info= Assembly.GetExecutingAssembly().GetType("Chapter19.Fanshe").GetMethod("Invoker");


            //typeof(int).GetTypeInfo().DeclaredMethods

            //var types = typeof(Console).Attributes;
            //var ms = MethodInfo.GetCurrentMethod().Attributes;
            //Console.WriteLine(types + "\r\n" + ms);
            ResolveEventHandler handler = (object sender, ResolveEventArgs args) => Assembly.ReflectionOnlyLoad(args.Name);

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += handler;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= handler;
        }


        


    }
}
