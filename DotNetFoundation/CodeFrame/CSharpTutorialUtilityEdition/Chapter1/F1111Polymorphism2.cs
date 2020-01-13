using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1112
{
    class F1112
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.11.1-2
        /// </summary>
        /// <param name="args"></param>
        ///
        static void Main(string[] args)
        {
            Person person1 = new Person("李四",18);
            Person.DisplayData(person1);

            Employee employee1 =new Employee("王五",88,"设计",100);
            Person.DisplayData(employee1);
        }

        
    }

    public class Person
    {
        private string _name = "张三";
        private int _age = 12;
        //类的虚方法
        protected virtual void Display()
        {
            Console.WriteLine("姓名：{0}；年龄：{1}",_name ,_age);
        }

        public Person(string Name, int Age)
        {
             _name= Name;
             _age =Age;
        }

        static public void DisplayData(Person aPerson)
        {
            aPerson.Display();
        }
    }

    public class Employee : Person
    {
        private string department;
        private decimal salary;

        public Employee(string Name, int Age, string D, decimal S) : base(Name, Age)
        {
            department = D;
            salary = S;
        }

        protected override void Display()
        {
            base.Display();
            Console.WriteLine("部门：{0}； 薪金：{1}",department,salary);
        }
    }
}