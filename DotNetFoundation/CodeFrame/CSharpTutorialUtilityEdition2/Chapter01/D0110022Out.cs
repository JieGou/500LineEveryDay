using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.10.2 方法的参数: 输出参数
     */
    class D0110022
    {
        public static void F1(ref char i)
        {
            i = 'b';
        }

        public static void F2(char i)
        {
            i = 'd';
        }

        public static void F3(out char i)
        {
            i = 'e';
        }

        public static void F4(string s)
        {
            s = "xyz";
        }

        public static void F5(g gg)
        {
            gg.a = 20;
        }

        public static void F6(ref string s)
        {
            s = "xyz";
        }

        static void Main(string[] args)
        {
            char a = 'c';
            string s1 = "abc";
            F2(a);
            Console.WriteLine(a);
            F1(ref a); //引用参数,函数修改外部的a的值.
            Console.WriteLine(a);
            char j;
            F3(out j);
            Console.WriteLine(j); //显示e
            F4(s1); //值参数,参数类型是字符串,s1为字符串引用变量
            Console.WriteLine(s1); //显示:abc, 字符串s1不被修改
            g g1 =new g();
            F5(g1); //值参数,但实参是一个引用类型变量
            Console.WriteLine(g1.a.ToString()); //显示20 ; 修改对象数据
            F6(ref s1); //引用参数,参数类型是字符串,s1为字符串引用变量
            Console.WriteLine(s1); //显示:xyz,字符串s1被修改

        }
    }

    class g
    {
        public int a = 0;
    }
}