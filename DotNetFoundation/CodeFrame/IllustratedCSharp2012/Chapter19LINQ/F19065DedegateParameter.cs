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
     * LINQ: 使用委托参数的示例
     */
    class F19065
    {
        static void Main(string[] args)
        {
            int[] intArray = new[] {3, 4, 5, 6, 7, 9};
            Func<int,bool> myDele =new Func<int, bool>(IsOdd);
            var countOdd = intArray.Count(myDele);

            Console.WriteLine($"Count of odd numbers:{countOdd}");

        }

        static bool IsOdd(int x)
        {
            return x % 2 == 1;
        }
    }
}