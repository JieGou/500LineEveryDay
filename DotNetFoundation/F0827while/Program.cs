using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0827while
{
    class Program
    {
        /// <summary>
        /// 输出10次 我不敢了
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int num = 1;

            while (num < 11)
            {
                Console.WriteLine("{0}: 我不敢了.", num);
                num++;
            }

            int num2 = 1;

            //do while 至少执行一次, while后跟的是继续循环额条件.
            do
            {
                Console.WriteLine("{0}: 我不敢了", num2);
                num2++;
            } while (num2 < 11);

            for (int num3 = 1; num3 < 11; num3++)
            {
                Console.WriteLine("{0}: 我不敢了.", num3);
            }

            Console.ReadKey();
        }
    }
}