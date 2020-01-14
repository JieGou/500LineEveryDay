using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /*
       *CSharpTutorialUtilityEdition C#教程实用版
       * 1.4.11  类型转换
     * C#语言类型转换分为:
     * 隐式转换
     * 显式转换: 又叫强制类型转换
     * 装箱: 值类型转换为object类型
     * 拆箱: object类型转换为值类型
       */
    class F10411
    {
        static void Main(string[] args)
        {
            //隐式转换
            int i = 10;
            long long1 = i;

            //显式转换
            long long2 = 5000;
            int i2 = (int)long2;

            //装箱
            int i3 = 10;
            object obj1 = i3;
            //值类型装箱后, 值类型变量的值不变,仅将这个值类型变量的值复制给这个object对象.

            //拆箱
            int i4 = 10;
            object obj2 = i4;
            int j1 = (int) obj2;

            // 装箱和拆箱的使用
            // void Display(object obj)
            // {
            //     int x = (int) obj;
            //     //函数的参数是object类型,可以将任意类型实参传递给函数.
            //     System.Console.WriteLine("{0},{1}",x,obj);
            // }


        }
    }
}