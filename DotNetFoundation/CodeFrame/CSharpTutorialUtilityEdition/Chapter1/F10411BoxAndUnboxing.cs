using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /*
       *CSharpTutorialUtilityEdition C#教程实用版
       * 1.4.10 字符串类
       */
    class F10410
    {
        static void Main(string[] args)
        {
            //字符串的自定
            string FireName = "Ming";
            string LastName = "Zhang";
            string Name = FireName + " " + LastName;
            char[] s22 = {'计', '算', '机', '科', '学'};
            string s3 =new string(s22);

            //字符串搜索
            string s = "ABC科学家";
            int i = s.IndexOf("科");

            //字符串比较函数
            string s1 = "abc";
            string s2 = "abc";
            int n = string.Compare(s1, s2);

            //判断字符串是否为空
            string s5 = "";
            // string s6 = " 不空";

            if (s5.Length==0)
            {
                s1 = "控";
            }

            //得到子字符串
            string s7 = "取子字符串";
            string s8 = s.Substring(2, 2); //从索引器第二个,取2个;
            char sc1 = s7[0]; //得到"取"

            //字符串删除
            string s9 = s7.Remove(0, 2); //从索引0开始,删除2个字符, s内容不变.

            //插入字符串
            string s10 = "计算机科学";
            string s11 = s.Insert(3, "软件"); //得到s1 ="计算机软件科学",s10内容不变

            //字符串替换函数
            string s12 = s10.Replace("计算机", "软件"); //得到软件科学, s10内容不变

            //把string转换为字符数组
            char[] sc2 = s10.ToCharArray(0, s10.Length);

            //把其他类型转换为字符串
            int i1 = 9;
            string s13 = i1.ToString();

            //大小写转换
            string s14 = "AaBbCc";
            string s15 = s14.ToLower();
            string s16 = s14.ToUpper();


        }
    }
}