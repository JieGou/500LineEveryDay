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
     * 1.9.1 属性
     */
    class D010902
    {
        static void Main(string[] args)
        {
            Person4 onePerson = new Person4();
            onePerson.Name = "田七";
            onePerson.Age = 20;
            int x = onePerson.Age;
            onePerson.Display();
        }
    }

    public class Person4
    {
        private string _name = "张三"; // _name 是私有字段
        private int _age = 12; //_age是私有字段

        public void Display()
        {
            Console.WriteLine($"姓名:{_name}; 年龄:{_age}");
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