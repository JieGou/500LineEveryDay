using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0905C04S06Ques05
{
    class F0905C04S06Ques05
    {
        /// <summary>
        /// 题5
        /// 结合使用while语句,break语句,continue语句,编写一个验证用户名的程序.
        /// 要求判断用户输入的次数,输入错误超过3次,则退出并给出提示
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int i = 1;

            do
            {
                Console.WriteLine("第"+i+"次,请你输入用户名:");
                string userName = Console.ReadLine();

                if (userName == "admin")
                {
                    Console.WriteLine("输入的用户名正确");
                    break;
                }

                else
                {
                    Console.WriteLine("输入的用户名错误");
                    i++;
                    continue;
                }

                
            } while (i < 4);
            Console.ReadKey();
            
        }
    }
}