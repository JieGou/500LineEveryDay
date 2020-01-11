using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
  static  class F0907C04S06Ques07b
    {
     public   static void run()
        {
            int oddSum = 0;
            int evenSum = 0;

            for (int i = 0; i <= 100; i++)
            {
                if (i % 2 == 0)
                {
                    oddSum = oddSum + i;
                }
                else
                {
                    evenSum = evenSum + i;
                }
            }

            Console.WriteLine("100以内,偶数的和是{0},奇数的和是{1}", oddSum, evenSum);
            Console.ReadKey();

        }
    }
}
