using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * LINQ: 使用Lamda参数的示例
     */
    class F19066
    {
        static void Main(string[] args)
        {
            int[] intArray = new[] {3, 4, 5, 6, 7, 9};
            var countOdd = intArray.Count(x => x % 2 == 1);

            Console.WriteLine($"Count of odd numbers:{countOdd}");
            Console.WriteLine("我是不要前缀也能运行的方法测试语句的结果的演示");


            //用匿名方法代替lamda表达式
            Func<int, bool> myDel = delegate(int x) { return x % 2 == 1; };
            var countOdd2 = intArray.Count(myDel);
            Console.WriteLine($"Count of odd numbers:{countOdd2}");
        }
    }
}