using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain3
{
    class F1226
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle();
            double x = 1;
            double y = 1;

            Console.WriteLine("面积是:{0}. \n周长是{1}", rect.GetArea(x, y), rect.GetArea(x, y));
        }
    }

    class Rectangle : IShape
    {
        public double GetArea(double x, double y)
        {
            double area = x * y;
            return area;
        }

        public double GetPerimeter(double x, double y)
        {
            double perimeter = 2 * x + 2 * y;
            return perimeter;
        }
    }
}