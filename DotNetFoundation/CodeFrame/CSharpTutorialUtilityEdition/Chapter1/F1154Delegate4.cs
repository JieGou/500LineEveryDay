using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
{
    class F1154
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// delegate的一个例子
        /// 委托的例子:下面的案例代码抄自
        /// https://www.cnblogs.com/wangdash/p/11737973.html
        /// </summary>
        static void Main(string[] args)
        {
            MyDelegateEvent delegateEvent =new MyDelegateEvent();
            delegateEvent.Show();

            Student student =new Student();

            Student.SayHiDelegate ChinaSayHi=new Student.SayHiDelegate(student.China);

            Student.SayHiDelegate JapanSayHi = new Student.SayHiDelegate(student.Japan);

            // student.SayHi();
            student.SayHiPerfact("张三", ChinaSayHi);
            student.SayHiPerfact("张三", JapanSayHi);

        }
    }


    public delegate void NoReturnNoPara();

    public delegate void NoReturnWhitPara(int x, int y);

    public delegate MyDelegateEvent WithReturnWithPara(out int x, ref int y);

    public class MyDelegateEvent
    {
        public void Show()
        {
            NoReturnNoPara method = new NoReturnNoPara(this.DoNothing);
            method.Invoke();
            IAsyncResult asyncResult = method.BeginInvoke(null, null);

        }

        public void DoNothing()
        {
            Console.WriteLine("this is Method: DoNothing.");
        }

        private MyDelegate ParaReturn(out int x, ref int y)
        {
            throw new Exception();
        }
    }

    class Student
    {
        public delegate void SayHiDelegate(string name);

        // public void SayHi()
        // {
        //     SayHiPerfact(" 王大师", this.China);
        // }

        public void SayHiPerfact(string name, SayHiDelegate method)
        {
            Console.WriteLine("我是通用方法");
            method.Invoke(name);
        }

        public void China(string name)
        {
            Console.WriteLine($"{name},早上好");

        }

        public void Britain(string name)
        {
            Console.WriteLine($"{name},Good morning");
        }

        public void Japan(string name)
        {
            Console.WriteLine($"{name}, おはよう");
        }
    }
}