using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.PracticeBookInFoundation.Day0216
{
    /*
     * 委托
     */
    //委托的声明: 带一个参数，无返回值的委托
    public  delegate void SayHelloDel(string name);

    public class T0216Delegate
    {
      
        // 事件是一个功能限制的委托变量，是一个委托类型 :
        public static event SayHelloDel SayHelloEvent;

        static void Main(string[] args)
        {
            //委托的声明
            //委托的使用方法：
            //委托的作用 ： 将方法以变量的形式传递，并以方法的形式执行。 【方法的参数和返回值需要与委托 一致】

            SayHelloDel dlg = SayHello;

            //委托链
            dlg += SayGoodbye;
            dlg -= SayGoodbye;
            dlg("老王");

            //匿名函数
            SayHelloDel dlg2 = delegate(string name) { Console.WriteLine($"{name},我是匿名函数"); };
            dlg2("匿名：");

            //lamda语句
            SayHelloDel dlg3 = (name) => { Console.WriteLine($"{name},我是lamba语句的匿名函数"); };
            dlg3("lamda");


            //事件： 
            //注册事件：
            SayHelloEvent += T0216Delegate_SayHelloEvent;
            //触发事件
            SayHelloEvent?.Invoke("老王");


            Console.ReadKey();
        }

        private static void T0216Delegate_SayHelloEvent(string name)
        {
            Console.WriteLine($"{name},我是事件。");
        }

        public static void SayHello(string name)
        {
            Console.WriteLine($"{name}, 你好!");
        }

        public static void SayGoodbye(string name)
        {
            Console.WriteLine($"{name}, 再见!");
        }
    }
}