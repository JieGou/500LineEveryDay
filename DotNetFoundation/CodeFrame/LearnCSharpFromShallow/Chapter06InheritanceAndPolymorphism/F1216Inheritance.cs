using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{

    class F1216
    {
        static void Main(string[] args)
        {
            Circle circle = new Circle();
            Console.WriteLine("圆的面积是：{0}", circle.GetArea());
        }
    }

    class Gragh
    {
        public double PI = Math.PI;
        public int r = 12;

        public double GetArea(double r)
        {
            double area = PI * r * r;
            return area;
        }
        public double GetArea(double width,double length)
        {
            double area =width*length;
            return area;
        }
    }

    class Circle : Gragh
    {
        public double GetArea()
        {
            double area = PI * r * r;
            return area;
        }
    }

    class Rectangle : Gragh
    {
        public double GetArea()
        {
            double area = 120;
            return area;
        }
    }
}