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
    class F19056
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
                orderby student.Age
                select student;

            foreach (var s in query)
            {
                Console.WriteLine($"{s.LName},{s.FName},{s.Age},{s.Major}");
            }

            var query2 = from s in students
                select s.LName;

            foreach (var q in query2)
            {
                Console.WriteLine(q);
            }
        }
        
    }
}