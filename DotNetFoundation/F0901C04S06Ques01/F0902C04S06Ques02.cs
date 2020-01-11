using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F0901C04S06
{
    /// <summary>
    /// 题2
    /// 使用if ...else if...else双重判断语句,检测用户输入的用户名是否存在,假设数据库中仅存在两个用户,admin 和user, 输入任何一个都可以检测到.
    /// </summary>
  static  class F0902C04S06Ques02
    {
   public     static void run()
        {
            Start:
            Console.WriteLine("\n请输入用户名");
            string str = Console.ReadLine();

            if (str == "admin")

            {
                Console.WriteLine("用户名正确;您好" + str);
            }
            else if (str == "user")

            {
                Console.WriteLine("用户名正确;您好" + str);
            }
            else
            {
                Console.WriteLine("用户名错误");
            }

            Console.ReadKey();
            goto Start;
        }
    }
}