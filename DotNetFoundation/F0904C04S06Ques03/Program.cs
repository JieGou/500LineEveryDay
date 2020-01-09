using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F0903C04S06Ques03
{
    /// <summary>
    /// 题3
    /// 使用if语句判断输入的用户名和密码是否正确,假设正确的用户名为admin ,密码为123
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            start:
            Console.WriteLine("\n请输入用户名");
            string userName = Console.ReadLine();
            Console.WriteLine("\n请输入密码");
            string passWord = Console.ReadLine();

            if (userName == "admin" && passWord == "123")
            {
                Console.WriteLine("用户名密码正确");
            }
            else
            {
                Console.WriteLine("用户名密码错误");
            }

            Console.ReadKey();
            goto start;
        }
    }
}