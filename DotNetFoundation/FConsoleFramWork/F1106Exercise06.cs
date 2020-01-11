using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题6
    /// 创建一个产品检测的类 Detection,
    /// 其中声明 N个关于产品的成员变量.
    /// 创建一个关于产品检测的方法Detection(),其参数表示个数是不确定性的产品
    /// </summary>
    class F1106
    {
        static void Main(string[] args)
        {
            Detection detec = new Detection();
            string productA = "产品A";
            string productB = "产品B";
            string productC = "产品C";
            detec.Detecting(productA, productB, productC);
            

        }
    }

    class Detection
    {
        public void Detecting(params string[] products)
        {
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine("开始检测产品{0}", products[i]);
            }
        }
    }
}