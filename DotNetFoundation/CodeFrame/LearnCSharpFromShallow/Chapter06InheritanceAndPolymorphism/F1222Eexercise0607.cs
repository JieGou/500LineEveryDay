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
    /// 题6 题7
    ///使用sealed关键字,定义一个文档类
    /// 在该类中,创建一个密封方法ReadDocument()
    /// </summary>
    class F1222
    {
        static void Main(string[] args)
        {
        }
    }

    sealed class Document
    {
        //密封文档类, 不能被继承
        //  sealed public void ReadDocument()
        //    {
        //   }
    }
}