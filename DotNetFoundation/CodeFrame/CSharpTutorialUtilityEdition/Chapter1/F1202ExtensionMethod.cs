using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /// <summary>
    /// 扩展方法练习
    /// 代码来源:https://www.cnblogs.com/wangdash/p/11825960.html
    /// </summary>
    class F1202
    {
        static void Main(string[] args)
        {
            int? i = 10;
            Console.WriteLine(i.ToInt());

            int? j = null;
            Console.WriteLine(j.ToInt());

            string text = "说好今天下雪,但是雪没有下来";
            Console.WriteLine(text.ToLength(10));

            string doc = "说好今天下雪,但是雪没有下来";
            Console.WriteLine(doc.ToLength(10));


        }
    }

    public static class ExtendMethod
    {
        //int 为空返回0
        //int? 表示可空的int
        //DateTime? 表示可空的时间类型
        public static int ToInt(this int? i)
        {
            return i ?? 0;
            //?? 空合并运算符, a??b ,当i为null时,返回右边的b;a不为null时,返回a本身;??运算顺序从右到左 a??b??c =a??(b??c)
        }

        public static string ToLength(this string text, int length = 15)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "空";
            }
            else if (text.Length > length)
            {
                return ($"{text.Substring(0, length)}...");
            }
            else
            {
                return text;
            }
        }
    }
}