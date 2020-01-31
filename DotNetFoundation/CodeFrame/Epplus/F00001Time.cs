using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /*
       *时间的练习
       */
    class F00001
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;

            string time = dt.DayOfYear.ToString()+dt.Hour.ToString()+dt.Minute.ToString()+dt.Second.ToString();
            Console.WriteLine(time);
        }
    }
}