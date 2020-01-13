using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1103
{
    class F11022
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition
        /// C#教程实用版
        /// 1.10.3
        /// 方法的参数为数组时, 也可以不是用params,此种方法可以使用一维或多维数组
        /// </summary>
        static void Main(string[] args)
        {

            int[,] a = { { 1, 2, 3 }, { 4, 5, 6 } };
            F(a); //实参为数组类引用的变量a

            //F(10,20,30,40); //此格式不能使用
            F(new int[,]{{60,70},{80,90}}); //实参为数组类引用
            //F(); //此格式不能使用
            //F(new int[,] {}); //此格式不能使用


            
        }
        //值参数,参数类型为数组类引用变量,
        static void F(int[,] args)
        {
            Console.WriteLine("Array contains{0} elements",args.Length);

            foreach (int i in args)
            {
                Console.WriteLine("{0}",i);
            }
           
        }
 
       

       
    }
}