using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1103
{
    class F1103
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition
        /// C#教程实用版
        /// 1.10.3
        /// MethodParameter
        /// </summary>
        static void Main(string[] args)
        {

            int[] a = { 1, 2, 3 };
            F(a); //实参为数组类引用变量a
            F(10,20,30,40); // 等价与 F(new int[] {10,20,30,40});
            F(new int[]{60,70,80,90}); //实参为数组类引用
            F();  //等价与F(new int[]{});
            F(new int[]{}); //实参为数组类引用, 数组无元素
        }


        //数组参数, 用params声明

        static void F(params int[] args)
        {
            Console.WriteLine("Array contains{0} elements:",args.Length);

            foreach (int i in args)
            {
                Console.Write("{0}",i);
                Console.WriteLine();

            }
        } 
    }
}