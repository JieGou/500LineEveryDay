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
     * 一个叫做Func的委托, 他接受带有两个形参和一个返回值的方法.
     * 方法返回的类型被标示为TR,方法参数类型被标识为T1和T2
     */
    class F17112
    {
        static void Main(string[] args)
        {

            //实例化委托
            var myDelegate4 = new Func<int, int, string>(Simple3.PrintString);

            //调用委托
            Console.WriteLine("Total: {0}",myDelegate4(14,13));
        }
    }
    //泛型委托
    public delegate string Func<T1, T2>(T1 p1, T2 p2);

    class Simple3
    {
        //方法匹配委托
        static public string PrintString(int p1, int p2)
        {
            int total = p1 + p2;
            return total.ToString();
        }

    }
    
    
}