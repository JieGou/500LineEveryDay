using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题9
    /// 创建一个MyClass，编写其构造函数和析构函数
    /// </summary>
    class F1109
    {
        static void Main(string[] args)
        {
            MyClass myClass = new MyClass(10);

        }
    }


    class MyClass
    {
        public MyClass(int i)
        {

        }

        ~MyClass()
        {
        }
    }
}