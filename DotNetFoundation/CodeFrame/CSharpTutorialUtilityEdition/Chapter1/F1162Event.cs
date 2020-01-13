using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
{
    class F1162
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.16.2  委托的声明
        /// 事件是C#语言内置的语法,可以定义和处理事件,为使用组件编程提供了良好的基础
        ///
        /// windows系统把用户的动作看做是消息,C#中称作事件.
        /// Windos操作系统负责统一管理所有的事件,把事件发送到各个运行程序. 各个程序用事件函数响应事件,这种方法叫事件驱动
        ///C#语言使用组件编制Windows应用程序. 组件本质上是类. 在组件类中,预先定义了该组件能够响应的事件,以及对应的事件函数.
        /// 该事件发生时,将自动调用自己的事件函数.
        /// 一个组件中定义了多个事件,应用程序不必也没必要响应所有的时间,而只需响应其中很少事件,程序员编制相应的事件处理函数.用来完成需要响应的事件所应完成的功能.
        /// 现在的问题是: 1 如果把程序员编制的事件处理函数和组件类中预先定义的事件函数联系起来
        ///             2 如何使不需要响应的事件无动作.
        /// 以上问题将在本节解决.
        /// </summary>
        /// <param name="args"></param>
        ///
        static void Main(string[] args)
        {
        }
    }
}