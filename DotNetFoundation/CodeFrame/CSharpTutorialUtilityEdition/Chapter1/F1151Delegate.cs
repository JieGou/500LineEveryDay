using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
{
    class F1151
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.15.1 delegate 委托
        /// 相当于C#中的函数指针;
        /// </summary>

        static void Main(string[] args)
        {
            MyClass p =new MyClass();

            //用new建立委托类MyDelegate对象,d中村中非静态的方法InstanceMethod()的地址
            //参数是被委托的方法d();
            MyDelegate d = new MyDelegate(p.InstanceMethod);
            d();
            d =new MyDelegate(MyClass.StaticMethod); //参数是被委托的方法
            d();  //调用静态方法
        }
    }
    //声明一个委托,注意声明的位置
    delegate int MyDelegate();

    public class MyClass
    {
        public int InstanceMethod()
        {
            Console.WriteLine("调用了非静态的方法");
            return 0;
        }

        static public int StaticMethod()
        {
            Console.WriteLine("调用了静态的方法");
            return 0;
        }
    }
}