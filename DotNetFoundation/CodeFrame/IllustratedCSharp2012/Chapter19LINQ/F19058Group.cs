using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * LINQ:
     */
    class F19058
    {
        static void Main(string[] args)
        {
            var students = new[]
            {
                new {LName = "Jones", FName = "Marry", Age = 19, Major = "History"},
                new {LName = "Smith", FName = "Bob", Age = 10, Major = "CompSci"},
                new {LName = "Fleming", FName = "Carol", Age = 21, Major = "History"}
            };

            var query = from student in students
                group student by student.Major;

            foreach (var s in query)
            {
                Console.WriteLine($"{s.Key}:"); //s.Key是分组的键值

                foreach (var t in s)
                {
                    Console.WriteLine($"    {t.LName},{t.FName}");
                }
            }
        }
    }
}