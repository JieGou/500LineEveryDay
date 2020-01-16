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
     * 查询语法和方法语法
     * 可以和lamda表达式结合使用
     */
    class F19012
    {
        static void Main(string[] args)
        {
            int[] numbers = {2, 5, 28, 31, 17, 16, 42};
            var numsQuery = from n in numbers
                where n < 20
                select n;
            var numsMethod = numbers.Where(x => x < 20);

            int numsCount = (from n in numbers where n < 20 select n).Count();

            foreach (var x in numsQuery)
            {
                Console.WriteLine($"{x}");
            }

            foreach (var x in numsMethod)
            {
                Console.WriteLine($"{x}");
            }

            foreach (var x in numsQuery)
            {
                Console.WriteLine($"{x}");
            }

            Console.WriteLine($"\n{numsCount}");
        }
    }
}