using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.LearningHardCSharpNote.Chapter9Event
{
    class CodeP94
    {
        static void Main(string[] args)
        {
            //初始化新郎对象
            Bridegroom2 bridegroom = new Bridegroom2();
            //实例化朋友
            Friend2 friend21 = new Friend2("张三");
            Friend2 friend22 = new Friend2("李四");
            Friend2 friend23 = new Friend2("王五");

            //使用+=来订阅事件

            bridegroom.MarryEvent += new Bridegroom2.MarryHandler(friend21.SendMessage);
            bridegroom.MarryEvent += new Bridegroom2.MarryHandler(friend22.SendMessage);

            bridegroom.MarryEvent += friend23.SendMessage;

            //发出通知
            bridegroom.OnBirthdayComing("朋友们,我生日快到了,来参加派对");

            Console.Read();
        }
    }

    //自定义事件类, 并使其自带事件数据
    public class MarryEventArgs : EventArgs
    {
        public string Message;

        public MarryEventArgs(string msg)
        {
            Message = msg;
        }
    }

    //新郎官类
    public class Bridegroom2
    {
        //自定义委托
        public delegate void MarryHandler(object sender, MarryEventArgs e);

        //定义事件
        public event MarryHandler MarryEvent;

        // 发出事件
        public void OnBirthdayComing(string msg)
        {
            //判断是否绑定了事件的处理方法
            if (MarryEvent != null)
            {
                MarryEvent(this, new MarryEventArgs(msg));
            }
        }
    }

    //朋友类
    public class Friend2
    {
        public string Name;

        public Friend2(string name)
        {
            Name = name;
        }

        //事件处理函数,该函数需要符合MarryHandler委托的定义
        public void SendMessage(object s, MarryEventArgs e)
        {
            //输出通知消息
            Console.WriteLine(e.Message);
            //对事件作出处理
            Console.WriteLine(this.Name + ": 收到了,到时参加");
        }
    }
}