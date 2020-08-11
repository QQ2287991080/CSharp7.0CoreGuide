using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
            //ResolveEventHandler handler = (object sender, ResolveEventArgs args) => Assembly.ReflectionOnlyLoad(args.Name);
            //AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += handler;
            //AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= handler;

            //Attribute.GetCustomAttributes()
            {

                //var dyMothod = new DynamicMethod("Foo", null, null, typeof(Fanshe));
                //ILGenerator gen = dyMothod.GetILGenerator();
                //gen.EmitWriteLine("Hello world");
                //gen.Emit(OpCodes.Ret);
                //dyMothod.Invoke(null, null);

                //int x = 2;
                //int y = 3;
                //x *= y;
                //Console.WriteLine(x);
                var dymethod = new DynamicMethod("TEST", null, null, typeof(void));
                ILGenerator gen = dymethod.GetILGenerator();

                LocalBuilder localX = gen.DeclareLocal(typeof(int));
                LocalBuilder localY = gen.DeclareLocal(typeof(int));

                gen.Emit(OpCodes.Ldc_I4, 2);
                gen.Emit(OpCodes.Stloc, localX);
                gen.Emit(OpCodes.Ldc_I4, 3);
                gen.Emit(OpCodes.Stloc, localY);

                gen.Emit(OpCodes.Ldloc, localX);
                gen.Emit(OpCodes.Localloc, localY);
                gen.Emit(OpCodes.Mul);
                gen.Emit(OpCodes.Stloc, localX);
                gen.EmitWriteLine(localX);
                gen.Emit(OpCodes.Ret);

                //dymethod.Invoke(null, null);
            }
            {
                //int x = 5;

                //while (x<=10)
                //{
                //    Console.WriteLine(x++);
                //}
                //动态生成代码
                var dymethod = new DynamicMethod("Foo", null, null, typeof(void));
                var gen = dymethod.GetILGenerator();

                //设置分支目标
                Label startLoop = gen.DefineLabel();
                Label endLoop = gen.DefineLabel();

                //声明本地变量x
                LocalBuilder x = gen.DeclareLocal(typeof(int));
                //将参数推入评估栈
                gen.Emit(OpCodes.Ldc_I4, 5);
                //相当于 int x=5
                gen.Emit(OpCodes.Stloc, x);
                //标记标签位置  {
                gen.MarkLabel(startLoop);

                gen.Emit(OpCodes.Ldc_I4, 10);
                gen.Emit(OpCodes.Ldloc, x);
                // 如果第一个值小于第二个值就分支  if(x>10)
                //如果 x>10 结束标签，循环结束
                gen.Emit(OpCodes.Blt, endLoop);
                //Console.WriteLine(x);
                gen.EmitWriteLine(x);
                //++x;
                gen.Emit(OpCodes.Ldloc, x);
                gen.Emit(OpCodes.Ldc_I4, 1);
                gen.Emit(OpCodes.Add);
                gen.Emit(OpCodes.Stloc, x);

                gen.Emit(OpCodes.Br, startLoop);
                gen.MarkLabel(endLoop);
                gen.Emit(OpCodes.Ret);

                dymethod.Invoke(null, null);
            }
        }





    }
}
