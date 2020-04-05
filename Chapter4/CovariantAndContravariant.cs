using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    /// <summary>
    /// 协变和逆变测试
    /// </summary>
   public class CovariantAndContravariant
    {
        public static void Invoker()
        {


            //{

            //    IEnumerable<string> str = new List<string> { "Zero", "One", "Two" };
            //    IEnumerable<object> obj = str;

            //    foreach (var item in obj)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}


            {

                //ConceteAnimal<Cat> cat = new ConceteAnimal<Cat>();
                //cat.Name(new Cat { Name = "猫" });
                //IAnimal<Animal> animal1 = cat;
                
            }

            {
                IComparer<string> comparer = Comparer<string>.Default;  
            }
            {

                ConceteAnimal<Animal> animal = new ConceteAnimal<Animal>();
                animal.Name(new Animal { Name = "狗" });
                IAnimal<Dog> dog = animal;
            }

            //Cat<string> cat = new Cat<string>();
            //Animals<object> animals = cat;
            //animals.AnimalType();
        }
        //public static void ListAnimals(Animals<string> animals)
        //{
        //    animals.AnimalType();
        //}
    }
    #region 协变
    /// <summary>
    /// 定义协变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public interface IAnimal<out T>:IAnimal
    //{
    
    //}
    /// <summary>
    /// 抽象动物
    /// </summary>
    public interface IAnimal
    {
        void Name(Animal animal);
    }
    /// <summary>
    /// 具体的动物
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConceteAnimal<T> : IAnimal<T>
    {
        public void Name(Animal animal)
        {
            Console.WriteLine($"这是{animal.Name}");
        }
    }
    //定义动物基类
    public class Animal
    {
        public string Name { get; set; }
    }
    public class Dog : Animal
    {

    }
    public class Cat : Animal
    {

    }
    #endregion
    #region 逆变
    /// <summary>
    /// 定义逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAnimal<in T> : IAnimal
    {

    }
    #endregion
}
