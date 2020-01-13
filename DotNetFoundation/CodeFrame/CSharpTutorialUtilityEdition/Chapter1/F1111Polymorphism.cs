using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1105
{
    class F1111
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.11.1
        /// 类的多态性:
        /// 一是编译是的多态: 方法的重载
        /// 二是运行是的多态,通过虚方法实现 virtual,同一个方法,参数也相同,不同对象调用出现不同的操作.
        /// </summary>
        /// <param name="args"></param>
        ///
        static void Main(string[] args)
        {
            B b = new B();
            A a1 = new A();
            A a2 = b;

            a1.F();
            a2.F();
            b.F();
            Console.WriteLine("\n");
            //非虚方法时, 按方法属于的类

            a1.virtualMethod();
            a2.virtualMethod(); //特殊:G()为虚方法,又a2引用派生类B对象,则调用派生类B的G()方法
            b.virtualMethod();
            Console.WriteLine("\n");
            //虚方法时,按对象属于的类
           
            F2(a1);
            F2(a2);
            F2(b);
        }

        //注意参数A为A类引用变量
        static void F2(A aA)
        {
            aA.virtualMethod();
        }
    }

    class A
    {
        public void F()
        {
            Console.WriteLine("A.F()");
        }

        public virtual void virtualMethod()
        {
            Console.WriteLine("A.virtualMethod()");
        }
    }

    class B : A
    {
        new public void F()
        {
            Console.WriteLine("B.F()");
        }

        public override void virtualMethod()
        {
            Console.WriteLine("B.virtualMethod()");
        }
    }
}