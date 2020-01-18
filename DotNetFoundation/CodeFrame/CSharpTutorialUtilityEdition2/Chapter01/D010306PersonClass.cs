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
     * 1.3.6 使用person类的完整例子
     */
    class D010306
    {
        static void Main(string[] args)
        {
            Person2 onePerson2 =new Person2("王五",15);
            onePerson2.Display();

            onePerson2.SetName("赵六");
            onePerson2.SetAge(26);
            onePerson2.Display();

            onePerson2 =new Person2();
            onePerson2.Display();
        }
    }

    class Person2
    {
        private string name = "张三";
        private int age = 12;

        public void Display()
        {
            Console.WriteLine($"姓名:{name},年龄:{age}");
        }

        public void SetName(string PersonName)
        {
            name = PersonName;
        }

        public void SetAge(int PersonAge)
        {
            age = PersonAge;
        }

        public Person2()
        {
            name = "田七";
            age = 12;
        }

        public Person2(string Name, int Age)
        {
            name = Name;
            age = Age;
        }
    }
}