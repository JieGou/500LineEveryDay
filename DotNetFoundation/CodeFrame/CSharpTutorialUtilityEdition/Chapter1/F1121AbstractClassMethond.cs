using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1121
{
    class F1121
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.12.1
        /// 抽象类和抽象方法
        /// 抽象类表示一种抽象的概念,有如下特点
        /// 1 抽象类只能作为其他类的基类,它不能被实例化
        /// 2 抽象类允许包涵抽象成员,虽然不是必须的,抽象成员用abstract修饰
        /// 3 抽象类不能同时是密封的
        /// 4 抽象类的基类也可以是抽象类;如果一个非抽象类的基类是抽象类,则该派生类必须覆盖来实现所有继承而来的抽象方法.
        ///     如果该抽象类从其他抽象类派生,应该包括其他抽象类中所有的抽象方法
        /// 基于抽象类的子类,重写抽象类的抽象方法时,调用的是抽象类的字段.
        /// </summary>
        /// <param name="args"></param>
        ///
        static void Main(string[] args)
        {
            Square square = new Square(20, 20);
            Circle circle = new Circle(1);
            square.Area();
            circle.Area();
        }
    }

    //定义抽象类
    abstract class Figure
    {
        protected double x = 0, y = 0;

        public Figure(double a, double b)
        {
            x = a;
            y = b;
        }

        //定义抽象方法， 无实现代码
        public abstract void Area();
    }

    class Square : Figure
    {
        public Square(double a, double b) : base(a, b)
        {
        }

        //重写基类的抽象方法
        public override void Area()
        {
            Console.WriteLine("矩形的面积是:{0}", x * y);
        }
    }

    class Circle : Figure
    {
        public Circle(double a) : base(a, a)
        {
        }

        public override void Area()
        {
            Console.WriteLine("圆形的面积是:{0}", Math.PI * x * y);
        }
    }
}