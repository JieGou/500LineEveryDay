using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F0830
    {
        static void Main(string[] args)
        {
            int i = 1;

            for (i = 1; i < 10; i++)
            {
                if (i == 8)
                {
                    goto Finish;
                }

                Console.WriteLine("循环次数: " + i.ToString());
            }

            Finish:
            Console.WriteLine("循环完7次后,终止循环");

            Console.ReadKey();
        }
    }
}