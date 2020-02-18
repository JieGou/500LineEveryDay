using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1

{
    /// <summary>
    /// 事件的例子:下面的案例代码抄自
    /// https://www.cnblogs.com/lishuyi/p/10765846.html
    /// 事件是C#的高级概念,当事件触发才执行我们所委托的方法
    /// 步骤:
    /// 1 创建一个委托;
    /// 2 将创建的委托与特定事件关联
    /// 3 编写C#事件处理程序
    /// 4 利用编写的C#事件处理程序,产生一个委托实例
    /// 5 把这个委托实例添加到产生事件对象的事件列表中去(+=).这个过程又叫订阅事件
    /// 点击才执行,不点击不执行
    ///
    /// 猫叫了(事件源), 老鼠跑了(事件订阅者),惊醒主人(事件订阅者)
    /// </summary>

     
    //发布者类: 发布者类里,定义事件,和触发事件的方法
    public class Cat
    {
        public event EventHandler<EventArgs> catEvent; 
        //事件需要声明在类里 ;(在发布者类里定义)
        //EventHandler是委托的类型.[这里的是系统的委托类型,不然要在类外声明委托]
        //EventArgs是参数类型,
        //catEvent是事件名称

        public void Cry(string msg)
        {
            Console.WriteLine(msg);
            catEvent(this, new EventArgs()); //触发事件
        }
    }

    //订阅者类 (响应事件的处理方法)
    public class Person3
    {
        public void Person(object sender, EventArgs e)
        {
            Console.WriteLine("人: 大半夜不睡觉,叫啥");
        }

    }

    public class Mouse3
    {
        public void Mouse(object sender, EventArgs e)
        {
            Console.WriteLine("老鼠: 有猫,快跑!");
        }
    }

    class F1166
    {
        static void Main(string[] args)
        {
             

        Cat cat1 = new Cat();
            Person3 person =new Person3();
            Mouse3 mouse =new Mouse3();

            cat1.catEvent += mouse.Mouse; //老鼠订阅订阅事件
            cat1.catEvent += person.Person; //人订阅事件
            //在类里订阅事件, 也可以在(订阅者类里订阅事件)

            cat1.Cry(" 猫: 喵喵~~~"); //猫动作，触发事件
        }

        // private static void Person(object sender, EventArgs e)
        // {
        //     Console.WriteLine("人: 大半夜不睡觉,叫啥");
        // }
        //
        // private static void Mouse(object sender, EventArgs e)
        // {
        //     Console.WriteLine("老鼠: 有猫,快跑!");
        // }
    }

   
}