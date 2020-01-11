using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题5
    /// 创建一个垃圾邮件类 JunkMail
    /// 构件一个清空所有邮件的方法 Empty()
    /// 其中参数为表示垃圾邮件数目的输出参数,讲成员变量mailNum重置为0;
    /// 
    /// </summary>
    class F1105
    {
        static void Main(string[] args)
        {
            int mailNum;
            JunkMail junk = new JunkMail();
            junk.Empty(out mailNum);
        }
    }

    class JunkMail
    {
        
        public void Empty(out int mailNum)
        {
            mailNum = 0;
        }
    }
}