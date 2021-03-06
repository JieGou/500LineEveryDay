﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.LearningHardCSharpNote.Chapter9Event
{
    class Code0901
    {
        static void Main(string[] args)
        {
            //初始化新郎官对象
            Bridgeroom bridgeroom = new Bridgeroom();
            //实例化朋友对象
            Friend friend1 = new Friend("张三");
            Friend friend2 = new Friend("李四");
            Friend friend3 = new Friend("王五");

            //使用+=定于事件
            bridgeroom.MarryEvent += new Bridgeroom.MarryHandler(friend1.SendMessage);
            bridgeroom.MarryEvent += new Bridgeroom.MarryHandler(friend2.SendMessage);

            //发出通知后,此时只有订阅了事件的对象才能收到通知  //不仅收到了通知,还传递了参数
            bridgeroom.OnMarriageComing("朋友们,我要结婚了,到时候来参加婚礼");
            Console.WriteLine("-----------------------------");

            Console.Read();
        }
    }

    //定义事件发布者类:新郎官类
    public class Bridgeroom
    {
        //自定义委托
        public delegate void MarryHandler(string msg);

        //使用自定义委托类型定义事件,事件名为MarryEvent
        public event MarryHandler MarryEvent;

        //发出事件
        public void OnMarriageComing(string msg)
        {
            //判断是否绑定了事件处理方法
            if (MarryEvent != null)
            {
                //触发事件
                MarryEvent(msg);
            }
        }
    }

    //事件的订阅者类:朋友类
    public class Friend
    {
        //字段 
        public string Name;

        //构造函数
        public Friend(string name)
        {
            Name = name;
        }

        //事件处理函数,该函数需要符合MarryHandler委托的定义, 即参数和返回值一致
        public void SendMessage(string message)
        {
            //输出通知信息
            Console.WriteLine(message + @"//不仅收到了通知, 还传递了参数");
            //对事件作出处理
            Console.WriteLine(this.Name + "收到了,到时候准时参加");
        }
    }
}