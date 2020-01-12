using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题7
    /// 创建一个邮件类 Mail, 其中添加一个接收邮件的方法.
    /// 改方法有一个接收邮件间隔时间的可选参数 interval, 默认值为5分钟
    /// </summary>
    class F1107
    {
        static void Main(string[] args)
        {
            Mail newMail = new Mail();
            newMail.ReceiveMail();

        }
    }

    class Mail
    {
        public void ReceiveMail(int interval = 5)
        {
        }
    }
}