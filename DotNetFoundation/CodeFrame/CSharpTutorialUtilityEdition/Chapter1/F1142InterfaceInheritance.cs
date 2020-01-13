using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
{
    class F1142
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

    interface IControl
    {
        void Paint();
    }

    interface ITextBox:IControl
    {
        void SetItems(string[] items);
    }

    interface IListBox:IControl
    {
        void SetItems(string[] items);
    }

    interface IComboBox:ITextBox,IListBox
    {
        //可以声明新方法
    }

   
}