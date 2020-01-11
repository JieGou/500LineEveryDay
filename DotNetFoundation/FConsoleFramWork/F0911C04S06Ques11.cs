using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
    /// <summary>
    /// 题11
    /// 编写控制台程序, 要求用户输入5个大写字母,如果用户输出的信息不满足要求,提示帮助信息并要求重新输入
    /// </summary>
 class F0911
    {
        static void Main(string[] args)
        {
            bool ok = false;

            while (ok == false)
            {
                string strN = Console.ReadLine();

                if (strN.Length != 5)
                {
                    Console.WriteLine("你输入的字符不是5个, 重输");
                }
                else
                {
                    ok = true;

                    for (int i = 0; i < 5; i++)
                    {
                        char c = strN[i];

                        if (c < 'a' || c > 'z')
                        {
                            Console.WriteLine("第{0}个字符{1},不是大写字母,请重新输入", i + 1, c);
                            ok = false;
                            break;
                        }
                    }
                }
            }

            Console.ReadKey();

        }
    }
}
