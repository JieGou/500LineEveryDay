using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0904C04S06Ques04
{
    /// <summary>
    /// 通过switch语句,判断用户选择的显示器和尺寸.可选择的尺寸有:17寸,19寸,20寸和22寸.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            strat:
            Console.WriteLine(@"请选择显示器尺寸: 
(1)17寸;
(2)19寸;
(3)20寸;
(4)22寸;");
            string size = Console.ReadLine();

            switch (size)
            {
                case "1":
                    Console.WriteLine("您选择了17寸");
                    break;

                case "2":
                    Console.WriteLine("您选择了19寸");
                    break;

                case "3":
                    Console.WriteLine("您选择了20寸");
                    break;

                case "4":
                    Console.WriteLine("您选择了22寸");
                    break;

                default:
                    Console.WriteLine("选的不对.");
                    break;
            }

            Console.ReadKey();

            goto strat;
        }
    }
}