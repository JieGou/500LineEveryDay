using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PracticeBook.Day0120
{
    /*
     * 练习7 : 引用参数
     *     
     */
    public class T012007
    {
        static void Main(string[] args)
        {
            double double1 = 100;
            double double2 = 10;
            Console.WriteLine($"Before: {double1};{double2}");
            double double3; //double3 没有复制,add函数也能工作.   //Console.WriteLine($" {double1};{double2}:{double3}");这句就不能工作.
            Helper0102.Add(ref double1, ref double2, out double3);
            Console.WriteLine($"After: {double1};{double2}:{double3}");

            double1 = 1000;
            Helper0102.Subtraction(ref double1);
            Console.WriteLine(double1);
        }
    }

    public static class Helper0102
    {
        public static void Add(ref double num1, ref double num2, out double num3)
        {
            num1 = num1 + 1;
            num3 = num1 + num2;
        }

        public static void Subtraction(ref double num1)
        {
            num1 = num1 - 1000;
        }
    }
}