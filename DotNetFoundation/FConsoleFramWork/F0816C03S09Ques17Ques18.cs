using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{/// <summary>
/// 题目17
/// 创建一个对象类型 obj, 初始值为ture
/// 使用is运算符检查对象obj是否为bool 类型;
///
/// 题目18
/// 创建一个对象类型Obj,初始值是一个字符串"helllo"
/// 使用as运算符,把对象obj转换为string 类型
/// </summary>
    class F0816C03S09Ques17Ques18
    {
        static void Main(string[] args)
        {
            object obj1 = true;
            bool result1 = obj1 is bool;
            Console.WriteLine(result1);


            object obj2 = "hello";
            string result2 = obj2 as string;
            Console.WriteLine(result2);
            Console.ReadKey();

        }
    }
}
