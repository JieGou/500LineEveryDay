using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMainF1103
{
    class F1103a
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition
        /// C#教程实用版
        /// 1.10.3
        /// 方法的参数为数组时, 也可以不是用params,此种方法可以使用一维或多维数组
        /// </summary>
        static void Main(string[] args)
        {
            UseMethod m = new UseMethod();
            UseMethod.StaticMethod(); //使用静态方法格式为: 类型.静态方法名
            m.InstanceMethod(); //使用实例方法格式为: 对象名.实例方法名
        }
    }

    public class UseMethod
    {
        private static int x = 0; //静态字段

        private int y = 1; //实例字段

        // 静态方法
        public static void StaticMethod()
        {
            x = 10; //正确,静态方法可以访问静态数据成员
            // y=20; //错误,静态方法不能访问实例数据成员
            Console.WriteLine(x);
        }

        public void InstanceMethod()
        {
            x = 10; //实例方法访问静态数据成员
            y = 20; //实例方法,访问实例数据成员
            Console.WriteLine("{0}\n{1}", x, y);
        }
    }
}