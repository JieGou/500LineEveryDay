using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.7.1 派生类的声明格式
     */
    class D010701
    {
        static void Main(string[] args)
        {
            Emplyee3 oneEmplyee = new Emplyee3("黄八", 30, "计算机", 3000);
            oneEmplyee.Display();
            Console.ReadLine();
        }
    }


    class Emplyee3 : Person
    {
        private string department;
        private decimal salary;

        //base 调用基类的属性
        public Emplyee3(string Name, int Age, string Department, decimal Salary) : base(Name, Age)
        {
            department = Department;
            salary = Salary;
        }

        //覆盖基类的Display()方法: 注意用new ,不可用override
        public new void Display()
        {
            base.Display(); //base调用基类的方法.
            Console.WriteLine($"部门:{department};薪金:{salary}");
        }
    }

    class Person3
    {
        private string _name;
        private int _age;

        public void Display()
        {
            Console.WriteLine($"姓名 :{_name}; 年龄:{_age}");
        }

        public Person3(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public Person3()
        {
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
    }
}