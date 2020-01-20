using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 练习5 :
     * 新建一个IList<type>  类型的集合 用linq扩展方法 筛选指定特征的元素。
     * 筛选条件分别调用 lambda表达式，
     * 匿名委托，
     * 自定义委托，
     * 预定义委托Fun<type,bool>       
     */
    class T01
    {
        static void Main(string[] args)
        {
            // //新建一个IList<type>类型的集合.
            IList<string> list = new List<string>() {"Amey", "Emmitt", "Ena", "Epps", "Enid", "Loomis"};

            // List<string> list = new List<string>(){"Amey", "Emmitt", "Ena", "Epps", "Enid", "Loomis"};
            ////上面两句效果好像是一样的

            list.Add("Lorna");
            Display2(list);

            //使用lamda表达式筛选
            var list1 = list.Where(m => m == "Amey");
            Display2(list1);

            //用匿名委托筛选
            var list2 = list.Where(delegate(string s)
            {
                if (s == "Amey") return true;
                else return false;
            });
            Display2(list2);

            // 使用自定义委托
            newDelegate newDelegate1 = new newDelegate(mathName);
            var list3 = list.Where(m => newDelegate1(m));
            Display2(list3);

            //预定义委托 Fun<type,bool>
            Func<string, bool> newFunc2 = mathName;
            var list4 = list.Where(m => newFunc2(m));
            Display2(list4);
        }
        
        public delegate bool newDelegate(string s);

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

        private static void Display2(IEnumerable<string> List)
        {
            Console.WriteLine("----------");

            foreach (var m in List)
            {
                Console.WriteLine(m);
            }
        }
    }
}