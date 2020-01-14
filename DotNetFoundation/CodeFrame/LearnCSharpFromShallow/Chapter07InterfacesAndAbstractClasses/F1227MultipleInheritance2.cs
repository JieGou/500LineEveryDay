using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain.LearnCSharpFromShallow.Chapter07
{
    class F1227
    {
        static void Main(string[] args)
        {
            Square square1 = new Square();
            string info = null;
            info += square1.GetSquareArea(1);
            square1.FillColor("green");
            Console.WriteLine("方形的面积是" + info);
        }
    }

    class Square : Rectangle, IGraph
    {
        public void FillColor(string sColor)
        {
            Console.WriteLine("填充颜色是{0}", sColor);
        }

        public double GetSquareArea(double x)
        {
            return base.GetArea(x, x);
        }
    }

    class Rectangele
    {
        public double GetArea(double x, double y)
        {
            double area = x * y;
            return area;
        }
    }

    interface IGraph
    {
        void FillColor(string sColor);
    }
}