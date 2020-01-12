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
    /// 题4
    /// 编写一个图形类Shape5, 创建两个参数不同的计算周长的方法,实现重载.一个方法计算矩形周长,另一个方法计算圆形的周长.
    /// </summary>
    class F1220
    {
        static void Main(string[] args)
        {
        }
    }

    class Shape5
    {
        public double GetArea(double r)
        {
            double area;
            return area = Math.PI * r * r;
        }

        public double GetArea(double x, double y)
        {
            double area;
            return area = x * y;
        }
    }
}