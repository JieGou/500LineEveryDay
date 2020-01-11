using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{/// <summary>
/// 题8
/// 九九乘法表
/// </summary>
 static   class F0908C04S06Ques08MultiplicationTable
    {
   public     static void run()
        {
            int i = 1;
            int j = 1;
            for (i = 1; i <=9; i++)
            {
                for (j = 1; j <=9&&j<=i; j++)
                {
                    Console.Write("{0}x{1}={2}\t",j,i,i*j);

                    if (j==i)
                    {
                        Console.WriteLine("\n");
                    }
                }
            }
            Console.ReadKey();

        }
    }
}
