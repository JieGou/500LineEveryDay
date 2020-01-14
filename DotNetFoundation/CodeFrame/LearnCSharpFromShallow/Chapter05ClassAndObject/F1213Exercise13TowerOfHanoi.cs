using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题13 汉诺塔
    /// 一个只能用递归解决的问题.
    /// 问题描述:
    /// 古代有个梵塔,塔内有三个座A B C; 开始时A座有64个盘,盘子大小不等,大的在下,小的在上.
    /// 有一个老和尚,想把这64个盘子,从A座移动到C座,但每次只允许移动一个盘,骑在移动过程中,在三个座上始终保持大盘在下,小盘在上.可以使用B座.
    /// </summary>
    class F1213
    {
        private static int count = 0;
        static void PrintMove(char x, char y)
        {
            Console.WriteLine("{0} --> {1}", x, y);
        }

        static void Hanoi(int n, char one, char two, char three)
        {
            if (n==0)
            {
                ++count;
                PrintMove(one,three);
            }
            else
            {
                ++count;
                Hanoi(n-1,one,three,two);
                //打印N个盘移动到目标塔的步骤
                PrintMove(one,three);
                //借助中间塔把原来第N个盘子上面的n-1个盘子,搬到现在第N个盘子所在塔上
                Hanoi(n-1,two,one,three);
            }
        }

        static void Main(string[] args)
        {
            int number = 0;
            Console.WriteLine("开始搬{0}个盘子",number+1);
            Hanoi(number,'A','B','C');
            Console.WriteLine("搬盘子结束,一共进行了{0}次搬运.",count);
        }
    }
}