using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.PracticeBookInFoundation.Day0207
{
    /*
     * select和where的区别
     */
    public class T0207SelectWhere
    {
        static void Main(string[] args)
        {
            List<Person> listP = new List<Person>()
            {
                new Person() {Name = "张三", age = 15},
                new Person() {Name = "李四", age = 16},
                new Person() {Name = "王五", age = 17},
                new Person() {Name = "赵六", age = 18},
                new Person() {Name = "田七", age = 19},
            };

            Console.WriteLine("Select操作符的使用:");

            var res = listP.Select(p => p);

            foreach (Person person in res)
            {
                Console.WriteLine(person.Name + "--" +person.age);
            }

            var res2 = listP.Select(p => new {p.Name});
            //将每一条数据进行投影到一张新的表中
            foreach (var item in res2)
            {
                Console.WriteLine(item.Name);
            }



            Console.WriteLine("Where操作符的使用:");
            var res3 = listP.Where(p => p.age >= 16);

            foreach (Person person in res3)
            {
                Console.WriteLine(person.Name +"---"+ person.age);
            }
            //Where()操作符是用于过滤用的，和sql中的where字句一样。

            Console.WriteLine("如果想过滤出年龄大于16的人,且只要他们的姓名字段形成一张新表:");
            //如果想过滤出年龄大于16的人,且只要他们的姓名字段形成一张新表
            var res4 = listP.Where(p => p.age >= 16).Select(p => new {p.Name});
            // new {p.Name}  是匿名类
            foreach (var item in res4)
            {
                Console.WriteLine("姓名" +item.Name +"--" );
            }

            Console.ReadKey();
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public int age { get; set; }
    }
}