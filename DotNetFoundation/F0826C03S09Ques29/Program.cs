using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0826C03S09Ques29
{
    class Program
    {
        /// <summary>
        /// 题29
        /// 使用三元运算符嵌套,计算折扣.
        /// 如果顾客消费满100元,打9折;
        /// 满300元,打8折;
        /// 满500元,打7折
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            strart:
            Decimal bill = new Random().Next(1, 1000);

            double discount = 1;

            if (bill >= 500)
            {
                discount = 7;
            }
            else if (bill < 500 && bill >= 300)
            {
                discount = 8;
            }
            else if (bill < 300 && bill >= 100)
            {
                discount = 9;
            }
            else
            {
                discount = 1;
            }

            Console.WriteLine("账单是:{0}元;\n折扣是{1}折", bill.ToString(), discount);
            Console.ReadKey();
            goto strart;
           
        }
    }
}