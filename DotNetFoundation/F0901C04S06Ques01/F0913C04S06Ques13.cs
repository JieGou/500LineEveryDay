using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
  static  class F0913C04S06Ques13
    {/// <summary>
    /// 题13
    /// 写一个控制台程序
    /// 要求完成以下的功能
    /// 接收一个整数n
    /// 如果接收的整数n为正值,输入1~n见的全部整数
    /// 如果接收的值为负值,用break或者return退出程序
    ///  </summary>
    /// <param name="args"></param>
   public     static void run()
        {
            int int1 =int.Parse(Console.ReadLine());

            if (int1<= 0)
            {
                return;
            }
            else
            {
                for (int i = 1; i < int1; i++)
                {
                    Console.Write(i+"\t");
                }
            }
            Console.ReadKey();


        }
    }
}
