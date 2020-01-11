using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0823C03S09Ques25
{
    class F0823
    {
        /// <summary>
        /// 题25
        /// 创建一个数组
        /// 数组的内容为5个任意字母,要求读取数组的第三个元素,并打印出来
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] newArray = new[] {"a", "b", "c", "d", "e"};
            Console.WriteLine(newArray[2]);
            Console.ReadKey();
        }
    }
}