using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeFrame.CShrapEssential.Chapter12
{
    /*
     * C# 本质论,代码片段12-3  带有委托参数的BubbleSort方法
     */
    class CodeList1203
    {
        static void Main(string[] args)
        {
            int[] numlist = new[] {1, 4, 7, 4, 8, 3};
            // SimpleSort2.BubbleSort(numlist, SimpleSort2.SortType.Descending);

            Console.WriteLine("排序前:");
            foreach (var i in numlist)
            {
                Console.WriteLine(i);
            }

            // DelegateSample.BubbleSort(numlist,DelegateSample.GreaterThan);

            // //lamda表达式
            // DelegateSample.BubbleSort(numlist, (int first, int second) => { return first < second; });

            //进一步简化,让编译器从委托类型推断参数类型
            DelegateSample.BubbleSort(numlist, (first, second) => first < second);

            Console.WriteLine("排序后:");
            foreach (var i in numlist)
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }
    }

    public class DelegateSample
    {
        public delegate bool ComparisonHandler(int first, int second);


        public static void BubbleSort(int[] items, ComparisonHandler comparisonMethod)
        {
            int i;
            int j;
            int temp;
            if (comparisonMethod == null)
            {
                throw new ArgumentException("comparisonMethod");
            }

            if (items == null)
            {
                return;
            }

            for (i = items.Length - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (comparisonMethod(items[j - 1], items[j]))
                    {
                        temp = items[j - 1];
                        items[j - 1] = items[j];
                        items[j] = temp;
                    }
                }
            }
        }


        public static bool GreaterThan(int first, int second)
        {
            return first > second;
        }
    }
}