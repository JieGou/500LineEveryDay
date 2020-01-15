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
    class F17121
    {
        static void Main(string[] args)
        {
            var trivInt =new Simple<int>();

            var trivString =new Simple<string>();

            Console.WriteLine("{0}",trivInt.ReturnIt(5));
            Console.WriteLine("{0}",trivString.ReturnIt("Hi,here"));
        }
    }
    //泛型接口
    interface IMyIfc<T>
    {
        T ReturnIt(T inValue);
    }
    class Simple<S> : IMyIfc<S>
    {
        public S ReturnIt(S inValue)
        {
            return inValue;
        }
    }


}