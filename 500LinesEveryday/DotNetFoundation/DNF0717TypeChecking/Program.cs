using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0717TypeCheckingIs
{/// <summary>
/// 类型转换检查运算符： is as
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            object obj = Math.PI;
            Console.WriteLine(obj is double);
            obj = "hello";
            Console.WriteLine(obj is double);
            Console.ReadKey();

        }
    }
}
