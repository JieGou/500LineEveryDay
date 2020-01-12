using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMainF1223
{
    /// <summary>
    ///第6章
    ///题8
    ///编写一个控制台应用程序.
    /// 1 创建一个类A, 在A中编写一个可以被重写的带int类型参数的方法MyMethod(),并在方法中输出传递的整数值+10后的结果
    /// 2 在创建一个类B,使其继承自类A,然后重写A中的MyMethod()方法,将A接收的整数值加50,并输出结果
    /// 3 在Main()方法中,实例化类A 类B, 并分别调用MyMethod()方法.
    /// </summary>
    class F1223
    {
        static void Main(string[] args)
        {
            A classA =new A();
            B classB =new B();
           
            Console.WriteLine(" classA.MyMethod(10)的结果是:{0}\n classB.MyMethod(10)的结果是{1}",
                classA.MyMethod(10),classB.MyMethod(10));
        }
    }

    public class A
    {
        public int MyMethod(int a)
        {
            return a + 10;
        }
    }

    public class B : A
    {
        public new int MyMethod(int b)
        {
            return b + 50;
        }
    }
}