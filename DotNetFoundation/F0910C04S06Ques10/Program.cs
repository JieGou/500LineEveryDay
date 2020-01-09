using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0910C04S06Ques10
{
    class Program
    {
        /// <summary>
        /// 题10
        /// 创建一个数组,并使用for语句,读取数组内的所有元素
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] intArray = {1, 2, 3, 4, 5};
            string[] strArray = {"a", "b", "c", "d", "e"};

            for (int i = 0; i < intArray.Length; i++)
            {
                Console.WriteLine(intArray[i]);
            }

            for (int i = 0; i < strArray.Length; i++)
            {
                Console.WriteLine(strArray[i]);
            }

            Console.ReadKey();
        }
    }
}