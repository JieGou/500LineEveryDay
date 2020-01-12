using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF192
{
    class F192
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition
        /// C#教程实用版
        /// 1.9.2
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Person onePerson = new Person();
            onePerson.Name = "田七";
            string s = onePerson.Name;

            onePerson.Age = 20;
            int x = onePerson.Age;
            onePerson.Display();

        }
    }


    public class Person
    {
        private string _pName = "张三"; //_PName是私有字段
        private int _pAge = 12; //_pAge是私有字段

        public void Display()
        {
            Console.WriteLine("姓名:{0};年龄:{1}",_pName,_pAge);
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

 
}