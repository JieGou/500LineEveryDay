using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F0820
    {
        /// <summary>
        /// 建立一个控制台应用程序, 根据用户输入的半径,计算圆形的面积和周长,结果保留4位小数,并打印到控制台中
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("输入圆形的半径:");
            double radius = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("圆形的半径是{0};\n圆形的面积是:{1};\n圆形的周长是{2};\n",
                Math.Round(radius, 4), Math.Round(Math.PI * radius * radius, 4), Math.Round(Math.PI * radius * 2, 4));

            Console.ReadKey();
        }
    }
}