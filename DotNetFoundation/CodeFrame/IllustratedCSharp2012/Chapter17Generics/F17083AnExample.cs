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
     * 泛型方法调用的一个案例
     */
    class F17083
    {
        static void Main(string[] args)
        {
            var intArray = new int[] {3, 5, 7, 9};
            var stringArray = new string[] {"first", "second", "third"};
            var doubleArray = new double[] {3.1, 2.1, 1.1};

            Simple.ReverseAndPrint<int>(intArray);
            Simple.ReverseAndPrint(intArray);

            Simple.ReverseAndPrint<string>(stringArray);
            Simple.ReverseAndPrint(stringArray);

            Simple.ReverseAndPrint<double>(doubleArray);
            Simple.ReverseAndPrint(doubleArray);
            //编译器推断泛型方法参数的类型
        }
    }

    class Simple
    {
        static public void ReverseAndPrint<T>(T[] arr)
        {
            Array.Reverse(arr);

            foreach (T item in arr)
            {
                Console.WriteLine($"{item.ToString()}");
            }
        }
    }
}