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
     * C# 本质论,代码片段12-1 BubbleSort1
     */
    class CodeList1201
    {
        static void Main(string[] args)
        {
            
        }
    }

    public static class SimpleSort1
    {
        public static void BubbleSort(int[] items)
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
                for (j = 1; j < i; j++)
                {
                    if (items[j - 1] > items[j])
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