using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0824C03S09Ques26
{
    class F0824
    {
        /// <summary>
        /// 题26
        /// 在控制台输出一个转义字符\t
        /// 题27 在控制台输出window的目录路径
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("\\t");
            Console.WriteLine(@"\t");

            string path = @"C:\temp\temp";
            Console.WriteLine(path);
            Console.ReadKey();
        }
    }
}