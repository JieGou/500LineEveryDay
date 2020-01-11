using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0718
{
    class DNF0718TypeCheckingAs
    {
        static void Main(string[] args)
        {
            object o1 = "some string";
            object o2 = 5;
            string s1 = o1 as string;
            string s2 = o2 as string;

            Console.WriteLine("字符串s1的值为：{0}", s1);
            Console.WriteLine("字符串s2的值为：{0}", s2);
            Console.ReadKey();
        }
    }
}