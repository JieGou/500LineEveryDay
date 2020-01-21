using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.IllustratedCSharp2012.Chapter13Delegate
{
    delegate void MyDel(int value); // 声明委托类型

    class F130101
    {
        void PrintLow(int value)
        {
            Console.WriteLine("{0} -Low Value",value);
        }

        void PrintHigh(int value)
        {
            Console.WriteLine("{0} - HighValue",value);
        }

        static void Main(string[] args)
        {
            F130101 program =new F130101();
            MyDel del; // 委托实例化(声明委托变量)

            //创建随机整数生成器对象,并得到0到99之间的一个随机数
            Random rand = new Random();
            int randomValue = rand.Next(99);

            //创建一个包含PrintLow或PrintHigh的委托对象,并将其复制给del变量
            del = randomValue < 50
                ? new MyDel(program.PrintLow)
                : new MyDel(program.PrintHigh);

            del(randomValue); //执行委托



        }
    }

}