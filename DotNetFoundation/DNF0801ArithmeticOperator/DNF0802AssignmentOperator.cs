using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0802AssignmentOperator
{
    class DNF0802AssignmentOperator
    {
        static void Main(string[] args)
        {
            //声明变量a和变量b
            double a = 10;
            double b = 5;

            //Assignment Operator 赋值运算符有
            //=
            //+=
            //-=
            //*=
            // /=
            // %=
            //本质 ： a  x=  b  等同于 a=  a x b 
            Console.WriteLine("{0}{1}{2}的结果是：{3}", a, "+=", b, a += b);

            a = 10;
            b = 5;
            Console.WriteLine("{0}{1}{2}的结果是：{3}", b, "+=", a, b += a);
            a = 10;
            b = 5;

            Console.WriteLine("{0}{1}{2}的结果是：{3}", a, "-=", b, a -= b);

            a = 10;
            b = 5;
            Console.WriteLine("{0}{1}{2}的结果是：{3}", b, "-=", a, b -= a);
            a = 10;
            b = 5;

            Console.WriteLine("{0}{1}{2}的结果是：{3}", a, "*=", b, a *= b);
            a = 10;
            b = 5;
            Console.WriteLine("{0}{1}{2}的结果是：{3}", b, "*=", a, b *= a);
            a = 10;
            b = 5;

            Console.WriteLine("{0}{1}{2}的结果是：{3}", a, "/=", b, a /= b);
            a = 10;
            b = 5;
            Console.WriteLine("{0}{1}{2}的结果是：{3}", b, "/=", a, b /= a);
            a = 10;
            b = 5;
            Console.WriteLine("{0}{1}{2}的结果是：{3}", a, "%=", b, a %= b);
            a = 10;
            b = 5;
            Console.WriteLine("{0}{1}{2}的结果是：{3}", b, "%=", a, b %= a);
            Console.ReadKey();
        }
    }
}