using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /// <summary>
    /// CSharpTutorialUtilityEdition(C#教程实用版)
    /// 1.17.1 索引指示器
    /// </summary>
    class F1171
    {
        static void Main(string[] args)
        {
            Team t1 =new Team();
            t1[0] = "张三";
            t1[1] = "李四";
            Console.WriteLine("{0},{1}",t1[0],t1[1]);
        }
    }

    class  Team
    {
        // 定义字符串数组,记录小组人员姓名
        string[] sName =new string[2];
        //索引指示器声明,this为Team类的对象
        public string this[int nIndex]
        {
            get { return sName[nIndex]; }
            set { sName[nIndex] = value; }
        }
    }
}