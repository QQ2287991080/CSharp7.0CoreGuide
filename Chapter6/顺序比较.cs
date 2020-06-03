using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    public class 顺序比较
    {
        public static void Invoker()
        {
            {
                var note = new Note(9);
                var note2 = new Note(10);
                var note3 = new Note(11);
                Console.WriteLine(note2.CompareTo(note3));//-1
                Console.WriteLine(note2.CompareTo(note2));//0
                Console.WriteLine(note3.CompareTo(note));//1
                Console.WriteLine(note3 > note2);//true
                Console.WriteLine(note2 < note);//false
            }
        }
    }
    //重写
    public struct Note : IComparable<Note>, IEquatable<Note>, IComparable
    {
        int _semitonesFromA;
        public int SemitonesFromA { get => _semitonesFromA; }

        public Note(int semitonesFromA)
        {
            _semitonesFromA = semitonesFromA;
        }
        /// <summary>
        /// 实现泛型接口
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Note other)
        {
            if (Equals(other)) return 0;
            return _semitonesFromA.CompareTo(other._semitonesFromA);
        }
        /// <summary>
        /// 实现非泛型接口
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Note))
                throw new InvalidOperationException("不是Note");
            return CompareTo((Note)obj);
        }
        /*重载“<”和“>”运算符*/
        public static bool operator >(Note n1, Note n2) => n1.CompareTo(n2) > 0;
        public static bool operator <(Note n1, Note n2) => n1.CompareTo(n2) < 0;
        /// <summary>
        /// 实现IEquatable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Note other) => other._semitonesFromA == this._semitonesFromA;
        /// <summary>
        /// 重写Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Note)) return false;
            return Equals((Note)obj);
        }
        /// <summary>
        /// 重写GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this._semitonesFromA.GetHashCode();
        }
        /*重载==和！=运算符*/
        public static bool operator ==(Note n1, Note n2) => n1.Equals(n1);
        public static bool operator !=(Note n1, Note n2) => !n1.Equals(n1);
    }

}
