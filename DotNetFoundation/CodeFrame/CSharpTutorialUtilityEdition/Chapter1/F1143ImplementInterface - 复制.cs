using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
{
    class F1143
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.14.1 接口的声明
        /// 注意：
        /// 接口成员默认访问方式是public，接口成员声明不能包括任何修饰符
        /// 接口的成员只能是 方法 属性 索引器 和事件， 不能是常量 ，域 操作符 构造函数
        /// </summary>
        /// <param name="args"></param>
        ///
        static void Main(string[] args)
        {
            Employee s =new Employee();
            s.Name = "田七";
            s.Age = 18;
            s.Salary = 2000;
            s.Display();
        }
    }

    interface I_Salary
    {
        decimal Salary { get; set; }
    }

    public class Person
    {
        private string _pName = "张三"; //_PName是私有字段
        private int _pAge = 12; //_pAge是私有字段

        public void Display()
        {
            Console.WriteLine("姓名:{0};年龄:{1}", _pName, _pAge);
        }

        // 定义属性Name
        public string Name
        {
            get { return _pName; }
            set { _pName = value; }
        }

        //定义属性Age
        public int Age
        {
            get { return _pAge; }
            set { _pAge = value; }
        }
    }

    public class Employee : Person, I_Salary
    {
        private decimal salary;

        public new void Display()
        {
            base.Display();
            Console.WriteLine("薪金:{0}",salary);
        }

        public decimal Salary
        {
            get { return salary; }

            set { salary = value; }
        }
    }
}