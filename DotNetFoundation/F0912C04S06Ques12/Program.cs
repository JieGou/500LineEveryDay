using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0912C04S06Ques12
{
    class Program
    {
        /// <summary>
        /// 题12
        /// 从N个整数中,找出最大的一个值
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] intArray = {1, 2, 3, 4, 55656, 76767, 34343, 767676, 33434, 67678111};
            int max;

            max = intArray[0];

            for (int i = 0; i < intArray.Length; i++)
            {
                if (max <= intArray[i])
                {
                    max = intArray[i];
                }
            }
            //也可以用写一个方法的方式来完成.

            Console.WriteLine(max);
            Console.ReadKey();
        }
    }
}