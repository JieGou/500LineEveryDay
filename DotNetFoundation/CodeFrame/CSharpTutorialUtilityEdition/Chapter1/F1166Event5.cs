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
    public class Cat
    {
        public event EventHandler<EventArgs> catEvent;

        public void Cry(string msg)
        {
            Console.WriteLine(msg);
            catEvent(this, new EventArgs());
        }
    }

    
    class F1166
    {
        static void Main(string[] args)
        {
            Cat cat1 = new Cat();
            cat1.catEvent += Mouse; //订阅猫事件
            cat1.catEvent += Person; //订阅人事件

            cat1.Cry(" 猫: 喵喵~~~"); //猫动作，触发事件
        }

        private static void Person(object sneder, EventArgs e)
        {
            Console.WriteLine("人: 大半夜不睡觉,叫啥");
        }

        private static void Mouse(object sender, EventArgs e)
        {
            Console.WriteLine("老鼠: 有猫,快跑!");
        }
    }
}