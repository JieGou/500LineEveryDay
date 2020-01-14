using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /*
       *CSharpTutorialUtilityEdition C#教程实用版
       * 1.4.5枚举类型
       */
    class F1045
    {
        enum Days
        {
            Sat = 1,
            Sun,
            Mon,
            Tue,
            Wed,
            Thu,
            Fri
        };
        static void Main(string[] args)
        {
            Days day = Days.Tue;
            int x = (int) Days.Tue;
            Console.WriteLine("day={0},x={1}",day,x);
        }
    }

   
}