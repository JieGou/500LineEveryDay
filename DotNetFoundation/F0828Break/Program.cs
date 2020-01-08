using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace F0828Break
{
    class Program
    {
        /// <summary>
        /// 该范例使用for循环写的计算循环次数的例子, 在循环中, 判断如果循环次数为8次,则使用break语句跳出循环.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int i = 0;

            for (i = 0; i < 10; i++)
            {
                Console.WriteLine("循环次数是" + i.ToString());

                //判断循环次数是否为8次
                if (i == 8)
                {
                    Console.WriteLine("循环8次后,跳出for语句");
                    break;
                    //相当于当i=8是,在第一个for里,写了个break,跳出for循环.
                }
            }
            Console.ReadKey();

        }
    }
}