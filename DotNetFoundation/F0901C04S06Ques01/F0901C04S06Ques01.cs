﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F0901C04S06
{
    /// <summary>
    /// 题1
    /// 使用if语句判断输入的用户名是否正确,正确的用户名为admiin ,输入错误要求有提示信息
    /// </summary>
  static  class F0901C04S06Ques01
    {
     public   static void run()
        {
            Strart:
            Console.WriteLine("请输入用户名");

            string str = Console.ReadLine();

            if (str == "admin")
            {
                Console.WriteLine("用户名正确");
            }
            else
            {
                Console.WriteLine("用户名错误");
            }

            Console.ReadKey();
            goto Strart;
        }
    }
}