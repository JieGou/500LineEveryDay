using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题11
    /// 一系类数的规则如下 1 1 2 3 5 8 13 21
    ///求滴30位数是多少, 用递归方法实现
    /// 
    /// </summary>
    class F1211
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Foo(30));
            }

        public static int Foo(int i)
        {
            if (i < 0)
            {
                return 0;
            }

            else if (i > 0 && i <= 2)
            {
                return 1;
            }
            else
            {
                return Foo(i - 1) + Foo(i - 2);
            }
        }
    }
}