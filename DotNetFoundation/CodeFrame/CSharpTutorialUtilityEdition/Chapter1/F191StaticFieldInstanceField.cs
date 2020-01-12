using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F191
    {
        /// <summary>
        /// CSharpTutorialUtilityEdition
        /// C#教程实用版
        /// 1.9.1
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Test t1 = new Test(100,200);
            
        }
    }

    public class Test
    {
        public const int intMax = int.MaxValue; //常量 必须赋初值;
        public int x = 0;  //实例字段
        public readonly int y = 0; //只读字段
        public static int cnt = 0; //静态字段
        public Test(int x1,int y1) //构造函数
        {
            // intMax =0 ;  //错误，不能修改常量
            x = x1; //在构造函数内,允许修改实例字段
            y = y1; //在构造函数允许修改只读字段
            cnt++; //没创建一个对象,都调用构造函数,用此语句可以记录对象的个数
                    }
        public void Modify(int x1,int y1)
        {
            //intMax =0; //错误,不能修改常量
            x = x1;
            cnt = y1;
            // y=10; 不允许修改只读字段
        }
    }

 
}