using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0817C03S09Ques19
{
    class Program
    {
        /// <summary>
        /// 通过用户输出的两个整数, 使用关系运算符, 判断两个数的大小
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            int a = Convert.ToInt32(Console.ReadLine()) ;
            int b = Convert.ToInt32(Console.ReadLine());

            if (a>b)
            {
                Console.WriteLine("a是{0},b是{1},{2}大于{3}",a,b,a,b);

            }
            else
            {
                Console.WriteLine("a是{0},b是{1},{2}不大于{3}",a,b,a,b);
            }
            Console.ReadKey();

        }
    }
}