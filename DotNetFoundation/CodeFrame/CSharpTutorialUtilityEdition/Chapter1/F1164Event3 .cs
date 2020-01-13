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
    /// https://www.cnblogs.com/wujuntian/p/10990342.html
    /// </summary>

    //声明委托类型
    delegate void Handler();

    //发布者类: 1 事件声明;  2 触发事件的代码
    class  InCrementer
    {
        //创建事件并发布: Handler 是委托类型,   CountedADozen是事件名
        public event Handler CountedADozen;


        //触发事件的代码
        public void DoCount()
        {
            for (int i = 0; i < 100; i++)
            {
                if ((i%2==0) &&(CountedADozen != null) )
                {
                    CountedADozen();
                }   
            }
        }

    }

    //订阅者类: 1 事件处理  2 程序声明
    class Dozens
    {
        public int DozensCount { get; private set; }

        public Dozens(InCrementer inCrementer)
        {
            DozensCount = 0;
            //订阅事件: 为事件增加处理程序
            inCrementer.CountedADozen += IncrementerDozenscount;
        }
        //声明事件处理程序
        void IncrementerDozenscount()
        {
            DozensCount++;
        }
    }

    class F1164
    {
     
  
        static void Main(string[] args)
        {
            //发布者类实例化
            InCrementer inCrementer =new InCrementer();

            //订阅者类实例化
            Dozens dozens =new Dozens(inCrementer);

            //触发事件
            inCrementer.DoCount();

            Console.WriteLine("Number of dozens = {0}",dozens.DozensCount);
        }
    }
}