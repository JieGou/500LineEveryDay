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
    /// 题5
    /// 创建一个页面类Page,内部使用Virtual 关键字,实现一个虚方法CreatePage()
    /// 创建一个基于页面类的派生类,NewsPage,并使用override关键字,重写CreatPage()方法
    /// </summary>
    class F1221
    {
        static void Main(string[] args)
        {
        }
    }

    class Page
    {
        public virtual void CreatePage()
        {
            //创建新页面
        }
    }

    class NewsPage:Page
    {
        public override void CreatePage()
        {
            //创建新页面2
        }
    }
}