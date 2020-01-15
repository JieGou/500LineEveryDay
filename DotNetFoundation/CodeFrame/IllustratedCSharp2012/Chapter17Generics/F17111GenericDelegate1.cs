using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * 泛型委托
     */
    class F1711
    {
        static void Main(string[] args)
        {
            //实例化委托
            var Mydel =new MyDelegate3<string>(Simple2.PrintString);
            //添加方法
            Mydel += Simple2.PrintUpperString;

            //调用委托.
            Mydel("Hi, There");
        }
    }

    delegate void MyDelegate3<T>(T value);

    class Simple2
    {
        static public void PrintString(string s)
        {
            Console.WriteLine(s);
        }

        static public void PrintUpperString(string s)
        {
            Console.WriteLine("{0}",s.ToUpper());
        }
    }
}