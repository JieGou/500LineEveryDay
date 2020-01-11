using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0807Chapter03Section09Question05And06
{
    class F0807
    {
        /// <summary>
        /// 使用一个字符的数组，在控制台输出一个hello
        /// 使用字符类型，声明一个中文字符 好
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char[] char1 =new char[]{'h','e','l','l','o'};
            char char2 = '好';

            Console.WriteLine(char1);
            Console.WriteLine(char2);
            Console.ReadKey();

        }
    }
}
