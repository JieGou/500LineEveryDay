using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0711
{
    class DNF0711Array
    {
        static void Main(string[] args)
        {
            //C#把数组(Array)看成一个带有方法和属性的对象
            //堆引用站直
            int[] nvar = new int[] {1, 2, 3};
            int[] nvar2 = {2, 3, 4};
           //数组作为对象， 常用的属性有 Length Rank
            //数组作为对象， 常用的方法是 .GetLength(int dimension)
            //数组作为对象， 常用的方法是 .GetLength(int dimension)
            string info = null;
            info += "数组的第三个值是：";
            info += nvar[2];
            info += ";数组所有维度的长度是：";
            info += nvar.Length;
            info += ";数组的秩序是：";
            info += nvar.Rank;

            info += ";数组的指定维的长度是：";
            info += nvar.GetLength(0);

            Console.WriteLine(info);
            Console.ReadKey();
        }
    }
}