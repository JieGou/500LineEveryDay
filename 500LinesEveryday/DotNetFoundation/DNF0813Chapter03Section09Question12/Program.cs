using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0812Chapter03Section09Question12
{
    /// <summary>
    /// 定义一个整数类型a, 初始值 0
    /// 用两种方法把变量a 转换为布尔类型的变量b
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            bool b = Convert.ToBoolean(a);
            bool c = bool.Parse(a.ToString());
        }
    }
}