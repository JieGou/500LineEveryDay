using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
{
    class F1141
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition(C#教程实用版)
        /// 1.14.1 接口的声明
        /// 注意：
        /// 接口成员默认访问方式是public，接口成员声明不能包括任何修饰符
        /// 接口的成员只能是 方法 属性 索引器 和事件， 不能是常量 ，域 操作符 构造函数
        /// </summary>
        /// <param name="args"></param>
        ///
        static void Main(string[] args)
        {
        }
    }

    public interface IExample
    {
        //所有接口的成员不能包括实现;
        //索引器声明
        string this[int index] { get; set; }

        //事件声明
        event EventHandler E;

        //方法声明
        void F();

        //属性声明
        string P { get; set; }
    }
}