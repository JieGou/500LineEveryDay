using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题14 
    /// 编写一个摇奖程序,可以一次产生8个随机数字.要求使用一种方法,返回8个参数
    /// </summary>
    class F1214
    {
        static void Main(string[] args)
        {
            int r1, r2, r3, r4, r5, r6, r7, r8;
            CreatNumber(out r1, out r2, out r3, out r4, out r5, out r6, out r7, out r8);
            Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}", r1, r2, r3, r4, r5, r6, r7, r8);
        }

        static void CreatNumber(out int r1, out int r2, out int r3, out int r4, out int r5, out int r6, out int r7,
            out int r8)
        {
            Random r = new Random();
            r1 = r.Next(10);
            r2 = r.Next(10);
            r3 = r.Next(10);
            r4 = r.Next(10);
            r5 = r.Next(10);
            r6 = r.Next(10);
            r7 = r.Next(10);
            r8 = r.Next(10);
        }
    }
}