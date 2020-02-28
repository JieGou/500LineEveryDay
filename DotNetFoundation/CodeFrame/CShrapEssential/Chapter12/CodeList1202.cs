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
     * C# 本质论,代码片段12-2 BubbleSort()方法,升序或降序
     */
    class CodeList1202
    {
        static void Main(string[] args)
        {
            int[] numlist = new[] {1, 4, 7, 4, 8, 3};
            SimpleSort2.BubbleSort(numlist, SimpleSort2.SortType.Descending);

            foreach (var i in numlist)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }

    public static class SimpleSort2
    {
        public enum SortType
        {
            Ascencding,
            Descending
        }

        public static void BubbleSort(int[] items, SortType sortOrder)
        {
            int i;
            int j;
            int temp;
            if (items == null)
            {
                return;
            }

            for (i = items.Length - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    bool swap = false;

                    switch (sortOrder)
                    {
                        case SortType.Ascencding:
                            swap = items[j - 1] > items[j];
                            break;
                        case SortType.Descending:
                            swap = items[j - 1] < items[j];
                            break;
                    }

                    if (swap)
                    {
                        temp = items[j - 1];
                        items[j - 1] = items[j];
                        items[j] = temp;
                    }
                }
            }
        }
    }
}