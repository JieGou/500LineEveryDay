﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.PracticeBookInFoundation.Day0206
{
    /*
     * enum的专题练习
     */
    public class T0206Enum
    {
        static void Main(string[] args)
        {
            int a = (int) Sex.女;
            Console.WriteLine("将枚举转换为整数:" + a);

            string b = Sex.女.ToString();
            Console.WriteLine("将枚举转换为字符串:" + b);

            Sex c = (Sex) Enum.Parse(typeof(Sex), "女");
            Console.WriteLine("将字符串转换为枚举:" + c.ToString());

            Sex c1 = (Sex) Enum.Parse(typeof(Sex), "女");
            Console.WriteLine("将字符串转换为枚举:" + (int) c1);

            Sex d = (Sex) 1;
            Console.WriteLine("将整数转换为枚举:" + d.ToString()); 

            string s = Enum.GetName(typeof(Sex), 1);
            Console.WriteLine("Enum.GetName方法:(参数 1)" + s);

            foreach (Sex sex in Enum.GetValues(typeof(Sex)))
            {
                Console.WriteLine("循环枚举value " + sex.ToString() + "=" + (int) sex);
            }

            foreach (var sex in Enum.GetNames(typeof(Sex)))
            {
                Console.WriteLine("循环枚举name " + sex);
            }

            var str = Enum.Format(typeof(Sex), 0, "d");
            Console.WriteLine(str);

            str = Enum.GetUnderlyingType(typeof(Sex)).ToString();
            Console.WriteLine(str);

            //判断 名称或者value 是否存在于枚举中
            var isornot = Enum.IsDefined(typeof(Sex), "Green");
            Console.WriteLine(isornot);

            var enm = Enum.Parse(typeof(Sex), "1");
            Console.WriteLine(enm);

            var str1 = Enum.ToObject(typeof(Sex), 2);
            Console.WriteLine(str1);

            int intVa = (int) Sex.男;
            Console.WriteLine(intVa);

            var intVa2 = Sex.女.ToString("D");
            Console.WriteLine(intVa2);

            Console.ReadKey();
        }
    }

    enum TraffiicLight
    {
        Green = 0,

        // Green是变量, 值是0; 不同变量的名字不能相同,值可以相同
        Yellow = 1,
        Red = -200
    }

    public enum Sex
    {
        [Description("man")] 男 = 0,
        [Description("woman")] 女 = 1
    }
}