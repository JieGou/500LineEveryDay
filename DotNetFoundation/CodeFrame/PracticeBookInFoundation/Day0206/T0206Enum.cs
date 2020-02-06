using System;
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
            // int a = (int) Sex.女;
            // Console.WriteLine("将枚举转换为整数:" + a);
            //
            // string b = Sex.女.ToString();
            // Console.WriteLine("将枚举转换为字符串:" + b);
            //
            // Sex c = (Sex) Enum.Parse(typeof(Sex), "女");
            // Console.WriteLine("将字符串转换为枚举:" + c.ToString());
            //
            // Sex d = (Sex) 1;
            // Console.WriteLine("将整数转换为枚举:" + d.ToString());
            //
            // string s = Enum.GetName(typeof(Sex), 1);
            // Console.WriteLine("降整数转换为字符串:" + s);
            //
            // foreach (Sex sex in Enum.GetValues(typeof(Sex)))
            // {
            //     Console.WriteLine("循环枚举value " + sex.ToString() + "=" + (int) sex);
            // }
            //
            // foreach (string sex in Enum.GetNames(typeof(Sex)))
            // {
            //     Console.WriteLine("循环枚举name " + sex);
            // }

            var str = Enum.Format(typeof(TraffiicLight), 0, "d");
            Console.WriteLine(str);

            str = Enum.GetName(typeof(TraffiicLight), 0);
            Console.WriteLine(str);

            str = Enum.GetUnderlyingType(typeof(TraffiicLight)).ToString();
            Console.WriteLine(str);

            //判断 名称或者value 是否存在于枚举中
            var isornot = Enum.IsDefined(typeof(TraffiicLight), "Green");
            Console.WriteLine(isornot);

            var enm = Enum.Parse(typeof(TraffiicLight), "1");

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