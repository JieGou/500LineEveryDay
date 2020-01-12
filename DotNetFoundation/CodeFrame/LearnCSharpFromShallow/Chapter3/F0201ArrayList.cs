using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F0201
    {
        static void Main(string[] args)
        {
            //创建集合对象
            ArrayList list = new ArrayList();
            ///集合: 很多数据的集合. 
            ///和数组的概念类型,比数组更优越
            ///数组:瓶颈:
            /// 1 长度不可变
            /// 2 类型单一
            ///集合:
            /// 长度可以任意改变, 类型随便
            list.Add(1);
            list.Add(3.14);
            list.Add(true);
            list.Add("张三");
            list.Add('男');
            list.Add(5000m);
            list.Add(new int[]{1, 2, 3, 4, 5, 6, 7 });
            
            //还能往里面加类
            Person p = new Person();
            list.Add(p);
            
            //添加自己
            list.Add(list);

            //看集合里面有什么
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            Console.ReadKey();
        }
    }

    public class Person
    {
        public void SayHello()
        {
            Console.WriteLine("我是人类");
        }
    }

}
