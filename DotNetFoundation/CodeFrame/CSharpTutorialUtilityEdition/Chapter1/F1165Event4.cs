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
    /// https://blog.csdn.net/weixin_41556165/article/details/82924684
    ///
    /// 事件是C#的高级概念,当事件触发才执行我们所委托的方法
    /// 步骤:
    /// 1 创建一个委托;
    /// 2 将创建的委托与特定事件关联
    /// 3 编写C#事件处理程序
    /// 4 利用编写的C#事件处理程序,产生一个委托实例
    /// 5 把这个委托实例添加到产生事件对象的事件列表中去(+=).这个过程又叫订阅事件
    /// 点击才执行,不点击不执行
    /// </summary>

        //事件发布者, sender
    class JudgeEvent
    {
        //定义委托
        public delegate void delegateClick();

        //定义一个事件
        public event delegateClick eventClick;

        //触发事件的方法: 当这个方法被执行时,意味着触发
        public void onClick()
        {
            eventClick(); //被引发的事件
        }
    }

    //定义订阅者 
    class DoClick
    {
        //定义事件处理的方法
        public void doC()
        {
            Console.WriteLine("鼠标被点击了");
        }
    }

    class F1165
    {
        static void Main(string[] args)
        {
            //实例化事件发布者
            JudgeEvent JudgeE = new JudgeEvent();

            //实例化事件订阅者
            DoClick runSport = new DoClick();


            //订阅事件
            JudgeE.eventClick +=runSport.doC;

            //引发事件
            JudgeE.onClick();

        }
    }
}