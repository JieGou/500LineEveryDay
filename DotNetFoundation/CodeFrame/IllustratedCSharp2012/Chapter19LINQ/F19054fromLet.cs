using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * LINQ:
     */
    class F19054
    {
        static void Main(string[] args)
        {
            var groupA = new[] {3, 4, 5, 6};
            var groupB = new[] {6, 7, 8, 9};

          //  //代码1
            // var someInts = from a in groupA
            //     from b in groupB
            //     where a > 4 && b <= 8
            //     select new {a, b, sum = a + b};
            //
            // foreach (var sum in someInts)
            // {
            //     Console.WriteLine(sum);
            // }

            // //代码2
            // var someInts2 = from a in groupA
            //     from b in groupB
            //     let sum = a + b //在新变量中保存结果
            //     where sum == 12
            //     select new {a, b, sum};
            //
            // foreach (var a in someInts2)
            // {
            //     Console.WriteLine(a);
            // }

          //代码3
          var someInts3 = from int a in groupA
              from int b in groupB
              let sum = a + b
              where sum >= 11
              where a == 4
              select new {a, b, sum};
          //new {a, b, sum} 是匿名类型

            foreach (var a in someInts3)
          {
              Console.WriteLine(a);
          }

        }
    }
}