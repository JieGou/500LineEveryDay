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
     * 泛型枚举接口
     */
    class F18042
    {
        static void Main(string[] args)
        {
            MyClass2 mc =new MyClass2();

            foreach (string color in mc)
            {
                Console.WriteLine(color);
            }
        }
    }

    class MyClass2 
    {
        public IEnumerator<string> GetEnumerator()
        {
            return BlackAndWhite(); //返回枚举器

        }
        //迭代器: 产生一个方法和一个枚举器
        //IEnumerator<string>  为返回枚举器
        public IEnumerator<string> BlackAndWhite()
        {
            yield return "black";
            yield return "gray";
            yield return "white";
        }
    }
}