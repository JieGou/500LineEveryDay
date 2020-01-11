using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0822C03S09Ques24
{
    class F0822
    {/// <summary>
     /// 题24
     /// 假设一光年是9.4605282x10的15次方,求10光年的距离
     /// </summary>
     /// <param name="args"></param>
        static void Main(string[] args)
        {
            double lightYear = 9.4605282 * Math.Pow(10, 15);
            Console.WriteLine(lightYear*10);
            Console.ReadKey();

        }
    }
}
