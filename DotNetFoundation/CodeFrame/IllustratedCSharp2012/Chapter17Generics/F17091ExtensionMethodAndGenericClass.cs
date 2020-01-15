using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * 扩展方法和泛型类
     * 扩展方法和泛型类结合使用,允许我们将类中的静态方法关联到不同的泛型上,
     * 还允许我们像调用类构造实例的实例方法一样调用方法.
     *  和非泛型类一样,泛型类的扩展方法:
     */
    class F17091
    {
        static void Main(string[] args)
        {
            var intHolder = new Holder<int>(3, 5, 7);
            var stringHolder = new Holder<string>("a", "b", "c");
            intHolder.Print();
            stringHolder.Print();
        }
    }

    static class ExtendHolder
    {
        public static void Print<T>(this Holder<T> h)
        {
            T[] vals = h.GetValues();
            Console.WriteLine("{0},\t{1},\t{2}", vals[0], vals[1], vals[2]);
        }
    }

    class Holder<T>
    {
        T[] Vals = new T[3];

        public Holder(T v0, T v1, T v2)
        {
            Vals[0] = v0;
            Vals[1] = v1;
            Vals[2] = v2;
        }

        public T[] GetValues() { return Vals; }
    }
}