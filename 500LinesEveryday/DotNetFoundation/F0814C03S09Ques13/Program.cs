using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0814C03S09Ques13
{/// <summary>
/// 定义一个整数类型的变量a, 初始值65(65是大写字幕A的ASCII码)
///把整数变量转化为大写字幕A
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int a = 65;
            char c = (char)a;
            Console.WriteLine(c);

            char d = Convert.ToChar(a);
            Console.WriteLine(d);



            Console.ReadKey();

        }
    }
}
