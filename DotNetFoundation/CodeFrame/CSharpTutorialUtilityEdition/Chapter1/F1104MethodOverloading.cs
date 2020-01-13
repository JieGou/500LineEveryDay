using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1104
{
    class F1104
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.10.4
        /// 方法的重载
        /// </summary>
        static void Main(string[] args)
        {
            UseAbs m = new UseAbs();
            int x = -1;
            long y = -123;
            double z = -23.333d;
            x = m.abs(x);
            y = m.abs(y);
            z = m.abs(z);
            Console.WriteLine("{0}\n{1}\n{2}", x, y, z);
            
        }
    }

    public class UseAbs
    {//整型数求绝对值
        public int abs(int x)
        {
            return (x < 0 ? -x : x);

        }
        //长整型数求绝对值
        public long abs(long x)
        {
            return (x < 0 ? -x : x);
        }
        //浮点数求绝对值
        public double abs(double x)
        {
            return (x < 0 ? -x : x);

        }
    }
   
    
}