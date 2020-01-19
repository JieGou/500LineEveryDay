using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.10.2 方法: 数组参数
     */
    class D0110021
    {
        static void F(params int[] args)
        {
            Console.WriteLine("Array contains {0} elements", args.Length);

            foreach (int i in args)
            {
                Console.WriteLine("{0}", i);
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int[] a = {1, 2, 3};
            F(10, 20, 30, 40);
            F();
            F(new int[] { });
        }
    }
}