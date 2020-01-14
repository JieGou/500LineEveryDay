using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    /*
       *CSharpTutorialUtilityEdition C#教程实用版
       * 1.4.1 C#的数类型
     * C#的数据类型分为三种:
     * 值类型,引用类型,指针类型   (站直堆引用)
     * 指针类型仅用于非安全代码中.
     *引用类型变量的赋值语句是传递对象的地址
       */
    class F1041
    {
        static void Main(string[] args)
        {
            MyClass4 r1 = new MyClass4(); //引用变量r1存贮啊MyClass类对象的地址
            MyClass4 r2 = r1; //r1 r2都代表同一个MyClass对象
            r2.a = 2; //等价与r1.a =2;
            f1();
        }

        static public void f1()
        {
            int v1 = 1; //值类型变量,值1 存储在栈中
            int v2 = v1; //
           
        }
    }

    class MyClass4
    {
        public int a = 0;
    }
}