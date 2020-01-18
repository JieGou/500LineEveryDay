using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.6.1程序控制语句
     */
    class D010601
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入要计算天数的月份");
            string month = Console.ReadLine();
            string info = "";

            switch (month)
            {
                case "1":
                case "3":
                case "5":
                case "12":
                    info = "31";
                    break;

                case "2":
                    info = "28";
                    break;

                case "4":
                case "6":
                case "9":
                    goto case "11";

                case "11":
                    info = "30";
                    break;

                default:
                    info = "输入错误";
                    break;
            }

            Console.WriteLine(info);
        }
    }
}