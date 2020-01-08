using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0714CaculateCircleArea
{
    class Program
    {
        /// <summary>
        /// 根据用户输出的半径，输出圆的面积
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //请用户输出圆的半径
            Console.WriteLine("请输入圆形的半径");
            decimal r = decimal.Parse(Console.ReadLine());
            decimal area = (decimal) Math.PI * r * r;
            Console.WriteLine("半径为{0}的圆的面积是{1}",r,Math.Round(area,4));
            Console.ReadKey();

        }
    }
}