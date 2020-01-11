using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0815C03S09Ques14
{
    class F0815
    {
        /// <summary>
        /// Q15
        /// 创建一个整数类型的变量a,初始值是10;
        /// 把变量a 装箱成一个对象类型
        /// Q16
        /// 定义一个对象类型obj, 初始值为1;
        /// 通过拆箱操作,把对象obj转换为bool类型;
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int a = 10;
            Object obj1 = a;

            object obj2 = 1;
            bool c = Convert.ToBoolean(obj2) ;

            Console.WriteLine(c);
            Console.ReadKey();

        }
    }
}