using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * LINQ:
     */
    class F19053
    {
        static void Main(string[] args)
        {
            Student5[] students = new Student5[]
            {
                new Student5 {StID = 1, LastName = "Carson"},
                new Student5 {StID = 2, LastName = "Klassen"},
                new Student5 {StID = 3, LastName = "Fleming"},
            };
            CourseStudent[] studentsIncourse = new CourseStudent[]
            {
                new CourseStudent {CourseName = "Art", StID = 1},
                new CourseStudent {CourseName = "Art", StID = 2},
                new CourseStudent {CourseName = "History", StID = 1},
                new CourseStudent {CourseName = "History", StID = 3},
                new CourseStudent {CourseName = "Physics", StID = 3},
            };

            //查找所有选择了历史的学生的姓氏
            var query = from s in students
                join c in studentsIncourse on s.StID equals c.StID
                where c.CourseName == "History"
                select s.LastName;
            //显示所有选择了历史课的学生的名字
            foreach (var q in query)
            {
                Console.WriteLine("Student taking History: {0}",q);
            }

        }
    }

    public class Student5
    {
        public string LastName;
        public int StID;
    }

    public class CourseStudent
    {
        public string CourseName;
        public int StID;
    }
}