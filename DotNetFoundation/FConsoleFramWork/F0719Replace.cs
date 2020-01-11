using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0719Replace
{
    class F0719
    {
        static void Main(string[] args)
        {
            string s1 = "要求派某个程序员写一个程序。";
            string sToReplace = "要求";
            string newStr = "ddddddddddd";
            string s2 = s1.Replace(sToReplace, newStr);
            Console.WriteLine("{0}\n{1}", s1, s2);
            Console.ReadKey();
        }
    }
}