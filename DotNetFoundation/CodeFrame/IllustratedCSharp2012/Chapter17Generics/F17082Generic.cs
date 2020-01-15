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
     * 泛型方法定义的调用
     */
    class F17082
    {
        static void Main(string[] args)
        {
            //调用
            Add<double>(3.3, 4);
            Add<int>(3,4);

        }
        //定义
        private static void Add<T> (T a, T b)
        {
            double sum = double.Parse(a.ToString()) + double.Parse(b.ToString());
            Console.WriteLine(sum);
        }
    }

   
   
}

