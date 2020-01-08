using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0705WinForExercise002
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// 题2
        /// 创建一个winForm窗体，窗体上放置按钮和标签控件， 并设置其属性
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
