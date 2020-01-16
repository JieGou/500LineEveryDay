using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter18
{
    /*
     * LINQ
     */
    class F19011
    {
        static void Main(string[] args)
        {
            int[] intArray = {1, 23, 4, 5, 67, 23, 4, 66, 5};
            IEnumerable<int> lowNum = from m in intArray
                where m < 24
                select m;

            foreach (int i in lowNum)
            {
                Console.WriteLine(i);
            }
        }
    }

    
}