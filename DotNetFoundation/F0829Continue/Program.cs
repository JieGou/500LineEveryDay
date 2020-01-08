using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0829Continue
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;

            for ( i = 0; i <10; i++)
            {
                Console.WriteLine("循环的第"+i+"次");
                //判断循环次数为8时
                if (i==8)
                {
                    continue;
                }
            }
            Console.ReadKey();

        }
    }
}
