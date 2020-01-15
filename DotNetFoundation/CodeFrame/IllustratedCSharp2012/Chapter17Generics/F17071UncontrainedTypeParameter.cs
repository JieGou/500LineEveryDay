using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * IllustratedCSharp2012
     * Chapter17.7.1 类型参数的约束
     * 给泛型添加额外的信息(约束 constrain),让编译器知道类型参数可以为哪些类型.
     *
     *
     */
    class F170761
    {
        static void Main(string[] args)
        {
        }
    }

    class MyGenericClass<T1, T2, T3>
        //T1 未绑定
        where T2 : Customer //只有Customer类型或者从Customer继承的类型的类才能用做类型实参.
        where T3 : IComparable //只有IComparable接口才能作为参数实参.

    {
    }

    class Customer
    {
    }
}