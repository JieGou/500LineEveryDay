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
     * 泛型接口
     */
    class F17122
    {
        static void Main(string[] args)
        {
            Simple4 trivial =new Simple4();
            Console.WriteLine("{0}",trivial.ReturnIt(5));
            Console.WriteLine("{0}",trivial.ReturnIt("Hi there"));
        }
    }

    interface IMyIfc2<T>
    {
        T ReturnIt(T inValue);
    }
    class Simple4 : IMyIfc2<int>, IMyIfc<string>
    {
        public string ReturnIt(string inValue)
        {
            return inValue;
        }

        public int ReturnIt(int inValue)
        {
            return inValue;
        }
    }
}