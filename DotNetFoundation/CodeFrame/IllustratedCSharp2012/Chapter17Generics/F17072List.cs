using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * List<T>的使用
     */
    class F17072
    {
        static void Main(string[] args)
        {
            string[] Arr = {"a", "b", "c"};

            //List<T> 将泛型类做成 构造类; 用构造类实例化出mList
            List<string> mList = new List<string>(Arr);

            //添加一个元素 List.Add(T item)
            mList.Add("d");

            //添加集合元素
            string[] Arr2 = {"f", "g", "h"};
            mList.AddRange(Arr2);

            //在index位置添加一个元素
            mList.Insert(1, "p");

            //遍历List中的元素
            foreach (var element in mList)
            {
                Console.WriteLine(element);
            }

            //删除元素
            mList.Remove("a");
            mList.RemoveAt(0);

            //判断某个元素是否在该List中
            if (mList.Contains("g")) Console.WriteLine(" g在集合中");
            else mList.Add("g");

            //给list里面元素排序
            mList.Sort();

            foreach (var element in mList)
            {
                Console.WriteLine(element);
            }

            //给list里面元素顺序翻转
            mList.Reverse();

            foreach (var element in mList)
            {
                Console.WriteLine(element);
            }

            //List.FindAll方法: 
            //List.Find方法
        }
    }
}