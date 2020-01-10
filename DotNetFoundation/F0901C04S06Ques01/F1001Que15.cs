using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F1001Que15
{
    class F1001Que15Class
    {
        ///题15
        /// 编写一个类似于购物街竞猜价格的游戏:
        ///随机取出1个数在1到100之间,要求输入一个1~100的数
        /// 然后输出提示: 如果与随机数一样,提示 "恭喜正确"
        /// 如果偏小,则提示"你输入的偏小"
        /// 如果偏大,则提示"你输入的偏大"
        static void Main(string[] args)
        {
            int rndNum = new Random().Next(1, 100);
            Console.WriteLine(rndNum);

            int a = 3;

            for (int i = a; i > 0; i--)

            {
                Console.WriteLine("请输入数字");
                int interNum = int.Parse(Console.ReadLine());

                if (interNum == rndNum)
                {
                    Console.WriteLine("恭喜你,猜对了");
                    break;
                }
                else if (interNum < rndNum)
                {
                    Console.WriteLine("偏小");
                }
                else
                {
                    Console.WriteLine("偏大");
                }
            }

            Console.WriteLine("\n!!!输入次数超过{0}次,退出",a);
            Console.ReadKey();

        }
    }
}