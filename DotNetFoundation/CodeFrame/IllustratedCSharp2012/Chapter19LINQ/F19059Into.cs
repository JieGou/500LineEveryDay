using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * LINQ:
     */
    class F19059
    {
        static void Main(string[] args)
        {
            var groupA = new[] {3, 4, 5, 6};
            var groupB = new[] {4, 5, 6, 7, 8};
            var someInts = from a in groupA
                join b in groupB on a equals b
                    into groupAgroupB
                from c in groupAgroupB
                select c;

            foreach (var s in someInts)
            {
                Console.WriteLine($"{s}");
            }
        }
    }
}