﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
    /// <summary>
    /// 求出100以内所有的质数
    /// </summary>
  static  class F1004Que18PrimeNumber
    {
        public static void run()
        {
            bool flag;
            int b = 200;

            for (int i = 2; i <= b; i++)
            {
                flag = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    Console.WriteLine("质数;" + i);
                }
            }

            Console.ReadKey();
        }
    }
}