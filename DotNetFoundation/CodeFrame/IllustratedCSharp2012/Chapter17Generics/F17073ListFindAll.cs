using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * List<T>的使用
     *FindAll方法和Find方法
     *
     */
    class F17073
    {
        static void Main(string[] args)
        {
            StudentClass stu =new StudentClass();
            stu.Name = "arron";
            List<StudentClass> students = new List<StudentClass>();
            students.Add(stu);
            FindName myName =new FindName("arron");
            students.Add(new StudentClass("candy"));

            foreach (var student in students.FindAll(new Predicate<StudentClass>(myName.IsName)))
            {
                Console.WriteLine(student);
            }

        }
    }

    public class StudentClass
    {
        public string Name
        {
            get;
            set;
        }

        public StudentClass()
        {
           
        }
        public StudentClass(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("姓名:{0}", Name);
        }

       
    }
    public class FindName
    {
        private string _name;

        public FindName(string Name)
        {
            this._name = Name;
        }

        public bool IsName(StudentClass s)
        {
            return (s.Name == _name) ? true : false;
        }
    }
}

