using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
{
    class F1155
    {
        /// <summary>
        /// 泛型委托的例子:
        /// https://www.cnblogs.com/wangdash/p/11741099.html
        /// </summary>
        static void Main(string[] args)
        {
            MyDelegate2 myDelegate2 =new MyDelegate2();
            myDelegate2.Show();
        }
    }

    class MyDelegate2
    {
        public void Show()
        {
            //Action Func 是Framework 3.0出现的
            //Action 系统提供0到16个泛型参数,不带返回值的委托
            Action action = DoNothing;
            Action<int> action1 = ShowInt;
            action1(1);

           //ction<int, string, Boolean, DateTime, long, int, string, Boolean, DateTime
           //   , long, int, string, Boolean, DateTime, long, string> action16 = null;

            //Func系统提供0~16个泛型参数,带泛型返回值的委托
            //Func 无参数有返回值.
            Func<int> func = Get ;
            int iResult = func.Invoke();

            //Func有参数有返回值
            Func<int, string> func1 = ToString;
            string sResult = func1.Invoke(1);

            // 多播委托有啥用呢? 一个委托实例包含多个方法,可以通过 += -=去增加/移除方法
            //invoke时,可以按顺序执行全部动作.
            //多播委托:任何一个委托都是多播委托的子类,可以通过+=去添加方法.
            //+= 给委托的实例添加方法,会形成方法链,Invoke时,会按顺序执行一系列方法.

            Action methods = DoNothing;
            methods += Study;
            methods += new MyDelegate2().StudyAdvanced;

            foreach (Action item in methods.GetInvocationList())
            {
                item.Invoke();
                item.BeginInvoke(null, null);
                
            }

            Console.WriteLine("分割线-------------------");

            //给委托的实例移除方法,从方法链的尾部开始匹配,遇到第一个吻合的移除,且只移除一个.
            //如果没有匹配,则什么都不会发生.
            methods -= DoNothing;
            methods -= Study;
            //去不掉因为是不同的实例的相同方法.
            methods.Invoke();

            // 一般多播委托是不带返回值的.


        }
        public void Study()
        {
            Console.WriteLine("Study Method;");
        }
        public void StudyAdvanced()
        {
            Console.WriteLine("StudyAdvanced Method;");
        }


        public string ToString(int i)
        {
            return i.ToString();

        }
        public int Get()
        {
            return 1; 
        }

        public void ShowInt(int i)
        {
            Console.WriteLine(i);
        }

        public void DoNothing()
        {
            Console.WriteLine("this is DoNothing Method");
        }
    }
}