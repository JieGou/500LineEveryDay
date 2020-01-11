using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
    class F1002
    {
        /// <summary>
        /// 题16
        /// 使用switch和goto语句,编写一个销售咖啡功能的代码.
        /// 根据咖啡的容量计算价格
        /// 小杯咖啡25元,中杯咖啡是在小杯的基础上加25元,大杯是在小杯的基础上再加50元.
        ///  </summary>
        /// <param name="arg"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("请输入您要的咖啡种类: 1 小杯; 2 中杯; 3 大杯");

            int choiceNum = int.Parse(Console.ReadLine());
            int cost = 0;

            switch (choiceNum)
            {
                case 1:
                    cost += 25;
                    break;

                case 2:
                    cost += 25;
                    goto case 1;

                case 3:
                    cost += 50;
                    goto case 1;

                default:
                    Console.WriteLine("选择错误");
                    break;
            }

            if (cost != 0)
            {
                Console.WriteLine("您的选择是{0},价格是{1}", choiceNum, cost);
            }

            Console.ReadKey();
        }
    }
}