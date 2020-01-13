using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter1
{
    class F1081
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition
        /// C#教程实用版
        /// 1.8.1 类的成员 ; 1.8.2类的字段和属性
        /// 类的成员包括以下类型:
        /// 局部变量: 在for switch等语句中和类方法中定义的变量,只在指定范围有效
        /// 字段: 即类中的变量和常量,包括静态字段,实例字段,常量和只读字段
        /// 方法成员: 包括静态方法和实例方法
        /// 属性: 按属性指定的get方法和set方法,对字段进行读写. 属性本质是方法
        /// 索引指示器: 允许像使用数组那样,访问类中的数据成员
        /// 操作符重载: 采用重载操作符的方法定义类中特有的操作
        /// 构造函数和析构函数:包含有可执行代码的成员被认为是类中的函数成员,
        /// 这些函数成员有方法 属性 索引指示器 操作符重载 构造函数和析构函数
        ///
        /// C#访问修饰符有private protected public 和 internal
        /// private : 声明私有成员,私有成员只能被类 内部的函数使用和修改;
        ///             私有函数成员只能被类内部的函数调用;
        ///     派生类虽然继承了基类的私有成员,但不能直接访问他们,只能通过基类的
        /// 的公有成员访问.
        /// protected: 声明保护成员,保护数据成员只能被类内部和派生类的函数使用和修改
        /// 保护函数成员只能被类内部和派生类的函数调用.
        /// public : 声明公有成员,类的公用函数可以被类的外部程序所调用.类的公用数据成员
        /// 可以被类的外部程序直接使用. 公有函数实际是一个类和外部通讯的接口,外部函数通过调用
        /// 公有函数,按照预先设计好的方法修改类的私有成员和保护成员.
        /// internal : 声明内部成员,内部成员只能子啊同一个程序集的文件中才可以访问.
        /// 一般是同一个应用Application或库 Library.
        /// 
        /// </summary>
        static void Main(string[] args)
        {
        }
    }
}