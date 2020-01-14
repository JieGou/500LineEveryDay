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
       * 1.4.3 结构类型
       */
    class F1043
    {
        static void Main(string[] args)
        {
            Point p1;
            p1.x = 166;
            p1.y = 111;

            Point p2;
            p2 = p1;

            //用new生成结构变量p3, p3仍为值类型变量.
            //使用new函数生成的结构变量p3,仅表示调用默认的构造函数,使x= y = 0;
            Point p3 =new Point(); 
            Console.WriteLine("{0},{1}",p3.x,p3.y);
        }
    }

    struct Point
    {
        public int x, y;  //结构类型中可以声明构造函数,变量不能赋初值.

    }
}