using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1105
{
    class F1105
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.10.5
        /// 操作符的重载:
        /// 允许重载的操作符: + - ! ~  == -- true false * / % & | ^  == !=  >= 
        /// 不允许重载的操作符:= && || ?:  new typeof sizeof is
        /// </summary>
        static void Main(string[] args)
        {
            Complex x = new Complex(1, 2);
            Complex y = new Complex(3, 4);
            Complex z = new Complex(5, 7);
            x.Display();
            y.Display();
            z.Display();
            z = -x;
            z.Display();
            z = x + y;
            z.Display();
        }
    }


    class Complex
    {
        //负数实部
        private double Real;

        //负数虚部
        private double Imag;

        //构造函数
        public Complex(double x, double y)
        {
            Real = x;
            Imag = y;
        }

        //重载医院操作符 负号
        static public Complex operator -(Complex a)
        {
            return (new Complex(-a.Real, -a.Imag));
        }

        // 重载二元操作符加号
        static public Complex operator +(Complex a, Complex b)
        {
            return (new Complex(a.Real + b.Real, a.Imag + b.Imag));
        }

        public void Display()
        {
            Console.WriteLine("{0}+({1})j", Real, Imag);
        }
    }
}