using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
    //发布者类: 1.1 声明事件; 1.2 触发事件的代码
    /*使用 EventHandler<TEventArgs>的原因
     * 由于不同的事件要传递的参数不同，更多时候是从EventArgs类派生的子类的实例，
     * 显然EventHandler委托时不能满足各种情况的。
     * 如果针对不同的事件也定义一个对应的委托，数量一旦多起来，不好管理，为了解决这个问题，
     * .NET类库提供了一个带有泛型参数的事件处理委托。
     * public delegate void EventHandler<TEventArgs>(object sender,TEventArgs e);
     * TEventArgs 是一个泛型参数
     */



//( 由于调用的是 系统定义的泛型委托类, 所以没有声明委托类型)
{
    //KeyPressedEventArgs是一个泛型参数， 继承自 EventArgs类。
    public class KeyPressedEventArgs : EventArgs
    {
        public ConsoleKey PressedKey { get; private set; }

        public KeyPressedEventArgs(ConsoleKey key)
        {
            PressedKey = key;
        }
    }


    public class EventSenderClass
    {
        // 1.1创建事件并发布:
        // EventHandler<KeyPressedEventArgs>带有泛型参数的事件类型
        // KeyPressed是事件名称
        public event EventHandler<KeyPressedEventArgs> KeyPressed;
        
  
        //触发事件的代码
        public void Start()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }

                //启动事件
                OnKeyPressed(new KeyPressedEventArgs(keyInfo.Key));
            }
        }


        //通过定义一个触发事件的方法
        protected virtual void OnKeyPressed(KeyPressedEventArgs e)
        {
            if (this.KeyPressed != null)
            {
                //KeyPressed是事件,
                //this是事件源即Sender,本处的EventSenderClass ??
                //e是事件的参数,可以继承自EventArgs类
                this.KeyPressed(this, e);
            }
        }
    }

    class F1163
    {
        /// <summary>
        /// 事件的例子:下面的案例代码抄自
        /// https://www.cnblogs.com/vickylinj/p/10922139.html
        /// </summary>
        /// <param name="args"></param>

        //建立一个订阅者类:  2.1 事件处理  2程序声明
        class SubscriberClass
        {
            //订阅者类的构造函数
            public SubscriberClass(EventSenderClass sender, KeyPressedEventArgs e)
            {
                sender.KeyPressed += app_KeyPressed(sender, e);
            }
            //订阅事件

            //2.1 事件处理函数
            void app_KeyPressed(EventSenderClass sender, KeyPressedEventArgs e)
            {
                Console.WriteLine("{0}", e.PressedKey.ToString());
            }
        }

        static void Main(string[] args)
        {
            //发布者类 实例化
            EventSenderClass sender = new EventSenderClass();

            //订阅者类 实例化
            SubscriberClass sub = new SubscriberClass(sender,e);

            //触发事件
            sender.Start();
        }
    }
}