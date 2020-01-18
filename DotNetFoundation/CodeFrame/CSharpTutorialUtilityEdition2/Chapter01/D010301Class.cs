using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.3.1 类的基本概念
     */
    class D010301
    {
        static void Main(string[] args)
        {
            Person zsPerson = new Person("张三", 16);
            Console.WriteLine($"{zsPerson.Name},{zsPerson.Age}");
        }
    }

    class Person
    {
        private string _name;
        private int _age;

        public void Display()
        {
            Console.WriteLine($"姓名 :{_name}; 年龄:{_age}");
        }

        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public Person()
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