using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0712TwoDimensionalArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 3;
            int n = 2; //设置数组大小
            int i, j;
            int[,] num = new int[m,n];
            //维度是 m (行)
            //秩 是  n （列）

            int y = 1;
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    num[i, j] = y; //辅助操作
                    y++;

                    Console.Write(num[i,j]+" ");  //不换行输出
                }

                Console.WriteLine("\n");  //换行
            }

            Console.WriteLine(num.Length+"\n"+num.Rank+"\n"+num.GetLength(1));
            Console.ReadKey();

        }
    }
}
