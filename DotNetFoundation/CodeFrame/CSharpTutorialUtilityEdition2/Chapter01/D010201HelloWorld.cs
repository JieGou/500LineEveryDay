using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 第一个程序
     */
    class D010201
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请键入你的姓名: ");
            string info = Console.ReadLine();
            Console.WriteLine($"欢迎! {info}");
        }
    }
}