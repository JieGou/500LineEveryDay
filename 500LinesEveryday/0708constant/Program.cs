using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0708constant
{
    class Program
    {
        static void Main(string[] args)
        {
            const double pi = 3.1415;

            //public  const double PI = 3.1415;  public关键字可选， 声明其作用范围
            //private 局部变量
            //protected 受保护的变量
            //internal 可以在一个链接库中范围
            //new 创建新变量，不继承父类同名变量。

            double r = 3.2;
            double area = pi * r * r;
            Console.WriteLine(area);
            Console.ReadKey();
        }
    }
}