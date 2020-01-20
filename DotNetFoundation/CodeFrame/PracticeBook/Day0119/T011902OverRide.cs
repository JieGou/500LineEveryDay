using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeBook.Day0119
{
    /*
     * 练习2 :  设计一个程序 用Object 作为基类，Animal 继承object   Person 继承 Animal ，  Ape   Cow   Sheep …… 都继承自Animal，最后实现的结果是 animal.sing() 可以出现符合各种动物的结果;
     */
    class T011902
    {
        static void Main(string[] args)
        {
            Animal animal = new Animal();
            Person person = new Person();
            Ape ape = new Ape();
            Cow cow = new Cow();
            Sheep sheep = new Sheep();

            Animal[] animals = {animal, person, ape, cow, sheep};

            foreach (Animal i in animals)
            {
                i.sing();
            }
        }
    }

    class Animal : Object
    {
        public virtual void sing()
        {
            Console.WriteLine("动物都会叫");
        }
    }

    class Sheep : Animal
    {
        public override void sing()
        {
            Console.WriteLine("绵羊咩咩叫");
        }
    }

    class Cow : Animal
    {
        public override void sing()
        {
            Console.WriteLine("奶牛哞哞叫");
        }
    }

    class Ape : Animal
    {
        public override void sing()
        {
            Console.WriteLine("猩猩进化成人也会唱歌");
        }
    }

    class Person : Animal
    {
        public override void sing()
        {
            Console.WriteLine("人会唱歌");
        }
    }
}