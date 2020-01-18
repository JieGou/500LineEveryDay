using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.6.2 foreach 语句
     */
    class D010602
    {
        static void Main(string[] args)
        {
            int[] list = {10, 20, 30, 40};

            foreach (int i in list)
            {
                Console.WriteLine($"{i}");
            }
        }
    }
}