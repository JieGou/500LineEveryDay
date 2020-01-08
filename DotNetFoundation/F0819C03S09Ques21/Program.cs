using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0819C03S09Ques21
{
    class Program
    {/// <summary>
    /// 利用算术运算符,计算长和宽为100和200的矩形面积
    /// </summary>
    /// <param name="args"></param>
        static void Main(string[] args)
    {

        double length = 200;
            double width = 100;
            Console.WriteLine("矩形的长是{0},宽是{1},面积是{2}",length,width,length*width);
            Console.ReadKey();
    }
    }
}
