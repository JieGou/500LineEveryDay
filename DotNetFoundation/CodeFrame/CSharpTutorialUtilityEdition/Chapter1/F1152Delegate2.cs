// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Reflection.Emit;
// using System.Text;
// using System.Threading.Tasks;
//
// public delegate void Mydelegate22(string s);
//
// namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
// {
//     class F1152
//     {
//         /// <summary>
//         /// CSharpTutorialUtilityEdition(C#教程实用版)
//         /// 1.15.1 delegate
//         /// 委托的例子:下面的案例代码抄自
//         /// https://www.cnblogs.com/vickylinj/p/10922139.html
//         /// </summary>
//         /// <param name="args"></param>
//         static void TestMethod1(string str)
//         {
//             Console.WriteLine("我是静态方法1:{0}", str);
//         }
//
//         static void TestMethod2(string str)
//         {
//             Console.WriteLine("我是方法2:{0}", str);
//         }
//
//         static void TestMethod3(string str)
//         {
//             Console.WriteLine("我是方法3:{0}", str);
//         }
//
//
//         static void Main(string[] args)
//         {
//             Mydelegate22 D1, D2, D3;
//             D1 = TestMethod1;
//             D2 = TestMethod2;
//             D3 = TestMethod3;
//
//             D1("1");
//             D2("2");
//             D3("3");
//         }
//     }
// }