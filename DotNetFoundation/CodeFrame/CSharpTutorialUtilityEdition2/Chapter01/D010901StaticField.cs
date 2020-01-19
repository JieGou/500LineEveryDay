using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.9.1 静态字段 实例字段 常量 和 只读字段
     */
    class D010901
    {
        static void Main(string[] args)
        {
            Test t1 = new Test(100,200);
            t1.x = 40; //引用实例的字段采用方法: 实例名.实例字段名
            Test.cnt = 0; //引用静态字段采用方法: 类名.静态字段名
            int z = t1.y; //引用只读字段
            z = Test.intMax; //引用常量

        }
    }

    public class Test
    {
        public const int intMax = int.MaxValue; //常量,必须赋初值
        public int x = 0; //实例字段
        public readonly int y = 0; //只读字段

        public static int cnt = 0; //静态字段, 属于类.

        //构造函数
        public Test(int x1, int y1)
        {
            // intMax =0 ;  //错误,不能修改常量
            x = x1; //在构造函数允许修改实例字段
            y = y1; //在构造函数允许修改只读字段
            cnt++; //每创建一个对象调用构造函数,用此语句可以记录对象的个数
        }

        public void Modify(int x1, int y1)
        {
            //intMax = 0; //错误,不能修改常量
            x = x1;
            cnt = y1;
            // y = 10; //不允许修改只读字段
        }
    }
}