using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
    /*使用 EventHandler<TEventArgs>的原因
     * 由于不同的事件要传递的参数不同，
     * 更多时候是从EventArgs类派生的子类的实例，
     * 显然EventHandler委托时不能满足各种情况的。
     * 如果针对不同的事件也定义一个对应的委托，数量一旦多起来，不好管理，为了解决这个问题，
     * .NET类库提供了一个带有泛型参数的事件处理委托。
     * public delegate void EventHandler<TEventArgs>(object sender,TEventArgs e);
     * TEventArgs 是一个泛型参数    
    
    事件的例子:下面的案例代码抄自  改成发布者 订阅者 模式
     https://www.cnblogs.com/vickylinj/p/10922139.html
    */

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


    //发布者类: 1.1 声明事件; 1.2 触发事件的代码
    public class EventSenderClass
    {
        // 1.1创建事件并发布:
        // EventHandler<KeyPressedEventArgs>带有泛型参数的事件类型
        // KeyPressed是事件名称
        public delegate void EventHandler<KeyPressedEventArgs>(object sender);

        public event EventHandler<KeyPressedEventArgs> KeyPressed;


        //事件源进行如下动作时,触发事件
        public void Start()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                KeyPressedEventArgs e = new KeyPressedEventArgs(keyInfo.Key);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }

                //启动事件
                if(KeyPressed!=null)
                this.KeyPressed(e);
                //OnKeyPressed(e);
            }
        }


        //通过定义一个触发事件的方法(虚方法可以被子类重写)
        protected virtual void OnKeyPressed(KeyPressedEventArgs e)
        {
            if (this.KeyPressed != null)
            {
                //如果这个类的xx事件存在,则执行下面的代码
                //KeyPressed是事件,
                //this是事件源即Sender,本处的EventSenderClass ??
                //e是事件的参数
                this.KeyPressed(this);
            }
        }
    }

    //建立一个订阅者类:  2.1 事件处理  2.2程序声明
    class SubscriberClass
    {
        public SubscriberClass(EventSenderClass sender)
        {
            //将方法加入事件的委托列表
            sender.KeyPressed += new EventSenderClass.EventHandler<KeyPressedEventArgs>(WhenKeyPressed);
        }


        void WhenKeyPressed(object sender)
        {
            Console.WriteLine("{0}按下了空格键,所以显示订阅者的显示事件方法", DateTime.Now.ToLongTimeString());
        }
    }


    class F1163
    {
        static void Main(string[] args)
        {
            //发布者类 实例化
            EventSenderClass sender = new EventSenderClass();

            //订阅者类 实例化
            SubscriberClass sub = new SubscriberClass(sender);

            //触发事件
            sender.Start();
        }
    }
}