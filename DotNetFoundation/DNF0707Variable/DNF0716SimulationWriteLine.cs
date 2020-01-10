using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0716SimulationWriteLine
{/// <summary>
/// 模拟writeLine（）方法， 在内部通过拆箱和的操作，返回字符串
/// </summary>
    class DNF0716SimulationWriteLine
    {
        public static string SimulationWriteLine(string format, object arg0)
        {
            //将任意值类型转换为string 类型 ：
            string arg = Convert.ToString(arg0);

            //使用RePlace()方法，将字符串中子字符替换为新的字符串
            //format = 笔记本的重量:{0}公斤, 将其中的{0} ，替换为arg值
            string s = format.Replace("{0}", arg);
            
            return s;
        }

        static void Main(string[] args)
        {
            string s = SimulationWriteLine("笔记本的重量:{0}公斤", 1.6);
            Console.WriteLine(s);
            Console.ReadKey();


        }
    }
}
