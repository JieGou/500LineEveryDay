using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * IllustratedCSharp2012
     * Chapter17.3 泛型类
     * 与普通的类,先声明,再实例化不同.
     * 泛型类是类的模板,先从泛型类构建实际的类类型,然后创建这个构建后的类类型的实例
     */
    class F17041
    {
        static void Main(string[] args)
        {
            SomeClass<short, int> generiClass1 = new SomeClass<short, int>();
            var genericClass2 = new SomeClass<short, int>();
            var second = new SomeClass<int, long>();

            SomeClass<short, int> myInst;
            myInst = new SomeClass<short, int>();
        }
    }

    public class SomeClass<T1, T2>
    {
        //T1 T2 是类型参数
        public T1 SomeVar;
        public T2 OtherVar;
    }
}