using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F0804
    {
        static void Main(string[] args)
        {
            int a = 10;
            int b = 20;

            string c = a > b ? "a大于b" : "b>a";
            Console.WriteLine(c);
            Console.ReadKey();

        }
    }
}