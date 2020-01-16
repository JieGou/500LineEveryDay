using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    class F18043
    {
        static void Main(string[] args)
        {
            MyClass4 mc =new MyClass4();

            foreach (string shade in mc)
            {
                Console.WriteLine("{0}",shade);
            }

            foreach (string shade in mc.BlackAndWhite())
            {
                Console.WriteLine("{0}",shade);
            }
        }
    }

    class  MyClass4
    {
        public IEnumerator<string> GetEnumerator()
        {
            IEnumerable<string> myEnumerable = BlackAndWhite(); //可枚举类型实例化
            return myEnumerable.GetEnumerator(); //获取枚举器
        }

        public IEnumerable<string> BlackAndWhite() //可枚举类型
        {
            yield return "black";
            yield return "gray";
            yield return "white";
        }
    }
}