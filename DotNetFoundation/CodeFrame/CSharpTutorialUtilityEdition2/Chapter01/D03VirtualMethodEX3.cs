using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 虚方法练习3:Shaper基类求面积,子类有正方形 圆形,重写基类的求面积
     */
    class D03
    {
        static void Main(string[] args)
        {
            Rectangle rec = new Rectangle(1, 1);
            Circle circle = new Circle(1);
            Shape shape1 = rec;
            Shape shape2 = circle;
            Console.WriteLine(shape1.GetArea());
            Console.WriteLine(shape2.GetArea());
        }
    }

    class Shape
    {
        public virtual double GetArea()
        {
            return 0;
        }
    }

    class Rectangle : Shape
    {
        private double _x;
        private double _y;

        public override double GetArea()
        {
            return _x * _y;
        }

        public Rectangle(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }

    class Circle : Shape
    {
        private double _x;

        public Circle(double x)
        {
            _x = x;
        }

        public override double GetArea()
        {
            return Math.PI * _x * _x;
        }
    }
}