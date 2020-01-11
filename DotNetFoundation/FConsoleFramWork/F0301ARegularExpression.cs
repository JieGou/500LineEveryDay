using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _0301ARegularExpression
{
    class F0301
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(Regex.IsMatch("p1g", "p[a-z0-9]g"));


            #region 匹配一个

            // Match mc = Regex.Match("今天是2019年4月18号", @"\d{4}");
            //
            // if (mc.Success)
            // {
            //     Console.WriteLine(mc.Value);
            // }

            #endregion


            #region 匹配多个

            //
            // MatchCollection mc = Regex.Matches("今天是2019年4月18号", @"\d{1,4}");
            //
            //
            // foreach (Match item in mc)
            // {
            //     if (item.Success)
            //     {
            //         Console.WriteLine(item.Value);
            //     }
            // }
            //

            #endregion

            #region replace

            //
            // string str = Regex.Replace("今天是2019年4月18号", @"\d{1,4}", @"[****]");
            //
            // Console.WriteLine(str);

            #endregion

            #region 字符串匹配的例子

            // bool mc1 = Regex.IsMatch("bbbbg", "^b.*g$");
            // bool mc2 = Regex.IsMatch("abg", "^b.*g$");
            // bool mc3 = Regex.IsMatch("gege", "^b.*g$");
            //
            // //判断是否为连续的英文字母
            // bool mc4 = Regex.IsMatch("123abcde3434", "[a-zA-Z]{5}");
            //
            //
            // //判断身份证: 省份证有18位 ,和15位
            // //130332198704534532
            // // 130332198704534533
            // //13090319871001
            //
            //
            // Console.WriteLine("\n\t" + mc1 + "\n\t" + mc2 + "\n\t" + mc3 + "\n\t" + mc4);

            #endregion

            #region 提取分组

            string regex = @"(\w+)@(\w+)((\.\w+){1,2})";

            Match mc = Regex.Match("abc123@qq.com.cn", regex);

            if (mc.Success)
            {
                //当前匹配的目标字符串
                //  Console.WriteLine(mc.Groups[0]);

                Console.WriteLine(mc.Groups[1].Value);
                Console.WriteLine(mc.Groups[2].Value);
                Console.WriteLine(mc.Groups[3].Value);
            }

            #endregion

            Console.ReadKey();
        }
    }
}