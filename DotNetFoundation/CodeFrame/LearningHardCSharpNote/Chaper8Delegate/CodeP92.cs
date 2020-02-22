using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.LearningHardCSharpNote.Chaper8Delegate
{
    class CodeP92
    {
        public delegate void GreetingDelegate(string name);

        static void Main(string[] args)
        {
            //引用委托之后
            CodeP92 p =new CodeP92();
            p.Greeting("张三",p.EnglishGreeting); 
            //方法变成了一个参数传给了greeting方法,是通过将防greeting方法定义是,将参数设置为委托实现的.
            p.Greeting("李四",p.ChineseGreeting);
            Console.Read();
        }

        public void Greeting(string name, GreetingDelegate callback)
        {
            //调用委托
            callback(name);
        }

        //英国人打招呼方法
        public void EnglishGreeting(string name)
        {
            Console.WriteLine("Hello" + name);
        }

        //中国人打招呼方法
        public void ChineseGreeting(string name)
        {
            Console.WriteLine("您好!" + name);
        }
    }
}