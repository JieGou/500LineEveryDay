using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 练习5 : List<> 和 IList<>
     * List<T> 是ArrayList类的泛型等效类.
     * 该类使用大小可按需动态增加的数组,实现 IList<T>泛型接口.
       
     */
    class D05
    {
        static void Main(string[] args)
        {
            IList<string> list = new List<string>() {"Amey", "Emmitt", "Ena", "Epps", "Enid", "Loomis"};

            List<string> list2 = new List<string>() {"Amey", "Emmitt", "Ena", "Epps", "Enid", "Loomis"};

            // 调试的结果, list1和list2的类型是一致的.
            // 接口可以作为返回值类型 ,  
            // 代表返回值都是这个接口的实现类的对象.
            //     说明,返回值是实现这一接口的某一类型的实例.
            //     此处代表list是 实现Ilist<string> 这一接口的 某个类型(List<string>)的实例

            //List的方法: 添加元素 
            list2.Add("张三");
            list2.Disply();
            //List 添加一组元素
            string[] temList = {"李四", "王五", "赵六"};
            list2.AddRange(temList);
            list2.Disply();
            //在指定位置添加一个元素
            list2.Insert(0, "田七");
            list2.Disply();

            //删除元素
            list2.Remove("张三");
            list2.Disply();
            //删除下标为index的元素
            list2.RemoveAt(0);
            list2.Disply();
            //从指定位置开始,删除多个元素
            // list2.RemoveRange(-2,2); //index需要大于0, 详 转到定义里看
            // list2.Disply();

            //排序元素 List.Sort(); List.Reverse();
            // list2.Reverse();
            // list2.Disply();

            //查找元素:  List.Find()  搜索与指定 谓词 所定义的条件相匹配的元素,返回第一个满足的元素
            //Predicate是对方法的委托
            // 委托个lamda表达式
            string list2Find2 = list2.Find(m => m.Length > 3);
            Console.WriteLine(list2Find2); //也是只返回第一个满足的
            //委托给一个函数
            string list2Find1 = list2.Find(ListFind);
            Console.WriteLine(list2Find1); //只返回第一个满足的

            //List.FindLast()

            //委托给一个函数
            List<string> subList1 = list2.FindAll(ListFind);
            subList1.Disply();
            //委托给一个lamda表达式
            List<string> subList2 = list2.FindAll(m => m.Length > 3);
            subList2.Disply();

            //排序
            List<int> list3 = new List<int>() {5, 1, 22, 11, 4};
            list3.Sort((x, y) => x.CompareTo(y));
            list3.Disply();
            list3.Sort((x, y) => -x.CompareTo(y));
            list3.Disply();

            //非基本类排序
            List<People2> list4 = new List<People2>();
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                int j = r.Next(0, 10);
                list4.Add(new People2(j, "name" + j));
            }

            Console.WriteLine("排序前 :");
            list4.Disply();
            Console.WriteLine("排序后 :");
            list4.Sort(); //排序,需要给people类增加接口: ICompare<People2> 原理看不懂.
            list4.Disply();
        }

        public static bool ListFind(string name)
        {
            if (name.Length > 3) return true;
            else return false;
        }
    }

    public static class Helper
    {
        public static void Disply<T>(this List<T> list)
        {
            Console.WriteLine("-----------------");

            foreach (T s in list)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("-----------------");
        }
    }

    //需要添加Icomparable接口
    class People2 : IComparable<People2>

    {
        private int _id;
        private string _name;

        public People2(int id, string name)
        {
            this._id = id;
            this._name = name;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int CompareTo(People2 other)
        {
            if (null == other)
            {
                return 1; //
            }

            return other.Id.CompareTo(this.Id); //降序
        }

        public override string ToString()
        {
            return "Id:" + _id + ";Name: " + _name;
        }
    }
}