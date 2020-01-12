using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1102
{
    class F1102
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition
        /// C#教程实用版
        /// 1.10.2
        /// MethodParameter
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char a = 'c';
            string s1 = "abd";
            F2(a); //值参数,不能修改外部的a
            Console.WriteLine(a);

            F1(ref a); //引用参数,函数修改外部a的值
            Console.WriteLine(a);
            char j;
            F3(out j); //输出参数,结果输出到外部变量j
            Console.WriteLine(j);
            F4(s1); //值参数,参数类型是字符串,s1为字符串引用变量
            Console.WriteLine(s1); //显示abc ;字符串s1不被修改
            g g1 =new g();
            F5(g1);// 值参数,但实参是一个类引用类型变量;
            Console.WriteLine(g1.a.ToString()); //显示: 20; 修改为对象数据
            F6(ref s1);
            Console.WriteLine(s1); //显示xyz, 字符串s1被修改

        }


        public static void F1(ref char i) //引用参数
        {
            i = 'b';
        }

        public static void F2(char i) // 值参数, 参数类型为值类型
        {
            i = 'd';
        }

        //输出参数
        public static void F3(out char i)
        {
            i = 'e';
        }

        //值参数,参数类型是字符串
        public static void F4(string s)
        {
            s = "xyz";
        }

        //值参数,参数类型是引用类型
        public static void F5(g gg)
        {
            gg.a = 20;
        }

        //引用参数,参数类型为字符串
        public static void F6(ref string s)
        {
            s = "xyz";
        }
    }

    class g
    {
        public int a = 0; //类定义
    }
}