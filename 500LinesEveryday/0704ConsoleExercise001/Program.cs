using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0704ConsoleExercise001
{
    /// <summary>
    /// 题目1
    /// 创建并编写一个简单的交互式控制台程序，要求用户输入自己的名字，然后打印出欢迎文字。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入你的名字");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, 欢迎光临", name);

            Console.ReadKey();
        }
    }
}