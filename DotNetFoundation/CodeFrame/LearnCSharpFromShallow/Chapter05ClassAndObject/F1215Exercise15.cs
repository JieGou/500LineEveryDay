using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题15
    /// 构建一个学生类,利用构造函数的,初始化学生的注册时间和入学年费
    /// </summary>
    class F1215
    {
        static void Main(string[] args)
        {
            Student2 st2 =new Student2();
            string info = null;
            info += "st2.dtStart: " + st2.dtStart + "\n";
            info += "st2.dtRegister: " + st2.dtRegister + "\n"; 
            Console.WriteLine(info);
        }

   
    }

    public class Student2
    {
        public DateTime dtStart;
        public DateTime dtRegister;

       public  Student2()
        {
            dtStart =DateTime.Now;
            dtRegister =DateTime.Now;
        }
    }
}