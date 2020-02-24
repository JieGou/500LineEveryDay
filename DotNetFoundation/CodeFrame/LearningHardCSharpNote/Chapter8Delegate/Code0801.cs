using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.LearningHardCSharpNote.Chapter8Delegate
{
    class Code0801
    {
        //1 使用delegate关键字来定义一个委托类型
        delegate void MyDelegate(int para1, int para2);

        static void Main(string[] args)
        {
            //2 声明委托变量d
            MyDelegate d;
            //3 实例化委托类型,传递的方法也可以是静态方法,这里传递的是实例方法.
            d = new MyDelegate(new Code0801().Add);

            //4 委托类型作为参数传递给另一个方法
            MyMethod(d);
            Console.Read();

        }

        void Add(int para1, int para2)
        {
            int sum = para1 + para2;
            Console.WriteLine("两个数的和为:" + sum);
        }

        private static void MyMethod(MyDelegate myDelegate)
        {
            //5 在方法中调用委托
            myDelegate(1, 2);

        }

        
    }
}