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
                Console.WriteLine(note2.CompareTo(note3));
                Console.WriteLine(note2.CompareTo(note2));
                Console.WriteLine(note3.CompareTo(note));
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
        public int CompareTo(Note other)
        {
            if (Equals(other)) return 0;
            return _semitonesFromA.CompareTo(other._semitonesFromA);
        }

        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Note))
                throw new InvalidOperationException("不是Note");
            return CompareTo((Note)obj);
        }

        public bool Equals(Note other) => other._semitonesFromA == this._semitonesFromA;
        public override bool Equals(object obj)
        {
            if (!(obj is Note)) return false;
            return Equals((Note)obj);
        }
        public override int GetHashCode()
        {
            return this._semitonesFromA.GetHashCode();
        }
        public static bool operator ==(Note n1, Note n2) => n1.Equals(n1);
        public static bool operator !=(Note n1, Note n2) => !n1.Equals(n1);
    }
}
