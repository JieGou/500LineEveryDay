﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
 static   class F0907C04S06Ques07
    {
        /// <summary>
        /// 题7
        /// 利用循环语句和判断语句,求100以内所有偶数和奇数的和.
        /// </summary>
        /// <param name="args"></param>
    public    static void run()
        {
            bool OddEven(int i, int b)
            {
                bool a = true;

                if (i % 2 == 0 && i <= b)
                {
                    a = true;
                }
                else
                {
                    a = false;
                }

                return a;
            }

            int max = 100;
            int oddSum = 0;
            int evenSum = 0;
            for (int i = 0; i <=max ; i++)
            {
                if (OddEven(i, 100))
                {
                    oddSum = oddSum + i;
                }
                else
                {
                    evenSum = evenSum + i;
                }
            }

            Console.WriteLine("在{0}以内,偶数的和是:{1};奇数的和是{2};",max,oddSum,evenSum);
            Console.ReadKey();


        }


       
    }
}