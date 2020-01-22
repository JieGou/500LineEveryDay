using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PracticeBook.Day0119
{
    /*
     * 练习1 :
     * 新建一个IList<type>  类型的集合 用linq扩展方法 筛选指定特征的元素。
     * 筛选条件分别调用 lambda表达式，
     * 匿名委托，
     * 自定义委托，
     * 预定义委托Fun<type,bool>       
     */
    public class T011901
    {
        static void Main(string[] args)
        {
            // //新建一个IList<type>类型的集合.
            IList<string> list = new List<string>() {"Amey", "Emmitt", "Ena", "Epps", "Enid", "Loomis"};

            // List<string> list = new List<string>(){"Amey", "Emmitt", "Ena", "Epps", "Enid", "Loomis"};
            ////上面两句效果好像是一样的

            list.Add("Lorna");
            GetHelp.Display2("list", list);

            //使用lamda表达式筛选
            var list1 = list.Where(m => m == "Amey");
            GetHelp.Display2("list1", list1);

            //用匿名委托筛选
            var list2 = list.Where(delegate(string s)
            {
                if (s == "Amey") return true;
                else return false;
            });
            GetHelp.Display2("list2", list2);

            // 使用自定义委托
            newDelegate newDelegate1 = new newDelegate(GetHelp.mathName);
            newDelegate1 += GetHelp.mathName2;
            var list3 = list.Where(m => newDelegate1(m));
            GetHelp.Display2("list3", list3);

            //预定义委托 Fun<type,bool>
            Func<string, bool> newFunc2 = GetHelp.mathName;
            var list4 = list.Where(m => newFunc2(m));
            GetHelp.Display2("list4",list4);

            //xu modify
            Func<string, bool> newFunc3 = GetHelp.mathName;
            var list5 = list.Where(GetHelp.mathName);
            GetHelp.Display2("list5",list5);
        }

        public delegate bool newDelegate(string s);
    }

    public static class GetHelp
    {
        public static bool mathName2(string s)
        {
            if (s.Length <= 4)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static bool mathName(string s)
        {
            if (s == "Amey")
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static void Display2(string s,IEnumerable<string> List)
        {
            Console.WriteLine("----------\n{0}:",s);

            foreach (var m in List)
            {
                Console.WriteLine(m);
            }
        }
    }
}