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
     * LINQ: 方法语法和扩展语法
     */
    class F19061
    {
        static void Main(string[] args)
        {
           int[] intArray =new int[]{3,4,5,6,7,9};
           //方法语法
           var count1 = Enumerable.Count(intArray);
           var firstNum = Enumerable.First(intArray);
           //扩展语法
           var count2 = intArray.Count();
           var firstNum2 = intArray.First();

           Console.WriteLine($"count:{count1},firstNums:{firstNum}");
           Console.WriteLine($"count:{count2},firstNums:{firstNum2}");

           int howMany = (from n in intArray
               where n<7
               select n).Count();
           Console.WriteLine($"count:{howMany}");

           var countOdd = intArray.Count(n => n % 2 == 1);
           Console.WriteLine($"count of odd numbers :{countOdd}");


        }
    }
}