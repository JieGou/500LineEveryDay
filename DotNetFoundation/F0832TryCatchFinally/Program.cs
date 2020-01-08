using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0832TryCatchFinally
{
    class Program
    {
        /// <summary>
        /// 将字符串类型转换整数类型,在转换过程中,捕获异常
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int i = 0;
            string str = "hello";

            try
            {
                i = int.Parse(str);

            }
            catch (Exception e)
            {
                Console.WriteLine("类型转换失败:" + e.Message);

            }
            finally
            {
                Console.WriteLine("转换后的结果:{0}", i);

            }
            Console.ReadKey();

        }
    }
}