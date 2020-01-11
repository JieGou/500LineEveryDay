using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0801ArithmeticOperator
{
    class F0801
    {
        static void Main(string[] args)
        {
            //声明变量a 和变量b ，并初始化
            int a = 12;
            int b = 2;
            int c = a % b;

            //将运算结果输出到控制台中
            Console.WriteLine("{0} 除以{1} 的余数是 {2}",a,b,c);

            Console.ReadKey();


        }
    }
}
