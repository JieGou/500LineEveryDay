// /// <summary>  
// /// 获取一个命名空间下的所有类  
// /// </summary>  
// /// <param name="name"></param>  
// /// <returns></returns>  
//
// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Runtime.CompilerServices;
// using System.Windows.Forms;
// using ClassMyTest;
// using RevitDevelopmentFoundation;
//
// namespace ExampleForReflection2
// {
//     class GetNameSpaces
//     {
//         static void Main(string[] args)
//         {
//             Assembly assembly =
//                 Assembly.LoadFile(@"D:\githubRep2\Gitee500LinesEveryday\DotNetRevit\R10PanelButtomTest\bin\Debug\R10PanelButtomTest.dll");
//
//             var types = assembly.GetTypes();
//
//             for (int i = 0; i < types.Length; i++)
//             {
//                 Console.WriteLine(types[i].Name);
//             }
//
//             Console.ReadKey();
//         }
//     }
// }

/*
 * 未完成
 */