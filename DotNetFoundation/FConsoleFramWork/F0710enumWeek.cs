using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    /// <summary>
    /// 星期枚举
    /// </summary>
    public enum enumWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

    class F0710
    {
        static void Main(string[] args)
        {
            //声明一个变量i， 并通过控制获取用户输出的信息
          start:
          int i = int.Parse(Console.ReadLine());
            //Parse tryParse
            //条件分支语句
            switch (i)
            {
                case (int)enumWeek.Saturday:
                    Console.WriteLine("张三的生日");
                    break;
                case (int)enumWeek.Monday:
                    Console.WriteLine("上班");
                    break;
                case (int)enumWeek.Thursday:
                    Console.WriteLine("去见客户");
                    break;
                default:
                    break;
            }
            Console.WriteLine("结束");
            //Console.ReadLine();
            goto start;
            //使用标签和goto 标签语句， 实现重复运行。
        }
    }
}