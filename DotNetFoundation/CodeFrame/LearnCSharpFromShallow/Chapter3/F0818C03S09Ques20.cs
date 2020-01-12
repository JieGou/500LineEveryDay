using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F0818
    {
        /// <summary>
        /// 通过用户输入的两个证书,求两个整数的余数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("a={0},b={1},a%b={2}", a, b, a % b);
            Console.ReadLine();
        }
    }
}