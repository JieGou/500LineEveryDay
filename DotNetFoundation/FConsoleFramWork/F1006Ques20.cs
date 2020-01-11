using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
class F1006
    {
        /// <summary>
        /// 使用异常捕获语句,尝试捕获除以0的异常
        /// </summary>
        /// <param name="arg"></param>
        static void Main(string[] args)
        {
            try
            {
                int i = 10;
                int j = 0;
                double k = i / j;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}