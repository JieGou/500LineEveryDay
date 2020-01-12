using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    ///第6章
    /// 题1
    /// 编写一个手机类,Cellphone,再编写一个苹果手机Iphone类,继承自手机类.
    /// 题2
    /// 在Cellphone中创建打电话的方法Call();
    /// 题3
    /// 在派生类Iphone中覆盖基类的方法
    /// </summary>
    class F1219
    {
        static void Main(string[] args)
        {
        }
    }

    class Cellphone
    {
      
        //手机类成员
        public void Call()
        {
            //打电话方法
        }
    }

    class Iphone : Cellphone
    {
        //Iphone类成员
        public new void Call()
        {

        }
  
    }
}