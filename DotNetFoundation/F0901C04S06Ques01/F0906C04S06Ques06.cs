using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{/// <summary>
/// 题6
/// 利用循环语句,求1+2+3+...+100的和
/// </summary>
  static  class F0906C04S06Ques06
    {
    public    static void run()
        {
            int i;
            int sum = 0;
            for (i = 1; i <= 100; i++)
            {
                sum = sum + i;
            }

            Console.WriteLine(sum);
            Console.ReadKey();


        }
    }
}
