using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0831Return
{
    class F0831
    {
        static double CaculateArea(double r)
        {
            return Math.PI * r * r;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("给个半径");
            double r = Convert.ToDouble(Console.ReadLine());
            double area = CaculateArea(r);
            Console.WriteLine(Math.Round(area, 4));
            Console.ReadKey();
        }
    }
}