using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /// <summary>
    /// CSharpTutorialUtilityEdition(C#教程实用版)
    /// 第一章练习
    /// </summary>
    class F1191
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入姓名:");
            string str = Console.ReadLine();
            Console.WriteLine("您好,{0}",str);

        }
    }
   
}