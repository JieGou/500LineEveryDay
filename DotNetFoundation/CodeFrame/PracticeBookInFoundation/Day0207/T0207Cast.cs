using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.PracticeBookInFoundation.Day0206
{
    /*
     * Cast的专题练习
     */
    public class T0207Cast
    {
        static void Main(string[] args)
        {
            ArrayList arraylist = new ArrayList();
            arraylist.Add("111");
            arraylist.Add("222333");
            arraylist.Add("333333333");

            IEnumerable<string> lists = arraylist.Cast<string>().Where(n => n.Length < 8);

            foreach (string list in lists)
            {
                Console.WriteLine(list);
            }

            string[] strArray = new string[] {"11", "22", "33"};
            var strType = strArray.GetType();
            Console.WriteLine(strType);
            Array strArray2 = new string[] {"33", "44", "55"};
            Console.ReadKey();
        }
    }
}