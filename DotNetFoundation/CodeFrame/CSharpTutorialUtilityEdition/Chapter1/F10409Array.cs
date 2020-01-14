// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
// {
//     /*
//        *CSharpTutorialUtilityEdition C#教程实用版
//        * 1.4.9 一维数组 和多维数组
//        */
//     class F1049
//     {
//         static void Main(string[] args)
//         {
//             //一维数组
//             int[] array = new int[3]; //用new运算符,建立一个3个元素的以为数组
//
//             for (int i = 0; i < array.Length; i++)
//             {
//                 array[i] = i * i;
//             }
//
//             for (int i = 0; i < array.Length; i++)
//             {
//                 Console.WriteLine("array[{0}]={1}", i, array[i]);
//             }
//
//             string[,] a3; //二维数组
//             string[][] j2;
//             string[][][][][] j3;
//             //在数组的声明的时候,可以对元素赋值
//             int[] a1 = new int[] {1, 2, 3};
//             //  in[] a3 = new int[3] {1, 2, 3}; 此格式不正确
//             int[] a4 = {1, 2, 3};
//         }
//     }
// }