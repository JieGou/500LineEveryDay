using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F0806
    {
        /// <summary>
        /// 声明一个字符类型的变量c， 并设置字符的值是字母a
        /// 声明一个字符类型的常量a,并赋值为大写字母A
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char c;
            c = 'a';
            Console.WriteLine(c);

            const char a = 'A';
            Console.WriteLine(a);

            Console.ReadKey();
        }
    }
}