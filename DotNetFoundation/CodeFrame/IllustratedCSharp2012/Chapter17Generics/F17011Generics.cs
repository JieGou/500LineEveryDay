// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace FConsoleMain.IllustratedCSharp2012.Chapter17
// {
//     /*
//      * IllustratedCSharp2012
//      * Chapter17 Generics
//      * 什么是泛型:
//      *  所有在类中用到的类型都是特定的类型-或是程序员定义的,或是语言或者BCL(基类库)定义的.
//      *  我们可以把类的行为提取或重构出来,使之不进能用到它们编码的数据类型上,还能引用到其他类型上, 类会更有用.
//      * 这时,我们需要泛型.
//      *泛型允许我们用类型占位符来写代码,然后在创建类的实例时指明真实的类型.
//      */
//     class F17011
//     {
//         static void Main(string[] args)
//         {
//         }
//     }
//
//     class MyStack<T>
//     {
//         private int StackPointer = 0;
//         private T[] StackArray;
//         public  void Push(T x) { }
//
//         public T pop()
//         {
//             
//         }
//
//     }
//
// }