using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
  public  static class F0914C04S06Ques14PerfectNumber
    {
        /// <summary>
        /// 求1000以内的所有"完数"
        /// 完数的定义: 一个数恰好等于它的所有因子之和.
        /// 尴尬,看不懂
        /// </summary>
        /// <param name="args"></param>
        public static void run()
        {
            for (int i = 2; i < 1000; i++)
            {
                int s = 1; //用于存放完整的各个因子
                string str = "1"; //用于存放各因子之和
                int a = 0;

                for (int j = 2; j <= (int) Math.Sqrt(i); j++)
                {
                    //如果j能被i整取且i和j的值不相等
                    if (i % j == 0 && i != j)
                    {
                        a = i / j; //用于粗放满足条件的两数之和
                        s += j + a; //存放因子之和
                        str += string.Format("+{0}+{1}", j, a); //存放满足条件的各个因子
                    }
                }

                //如果满足条件的各个因子之和与原值相等,则输出
                if (s == i) Console.WriteLine("{0}={1}", i, str);
            }

            Console.ReadKey();
        }
    }
}