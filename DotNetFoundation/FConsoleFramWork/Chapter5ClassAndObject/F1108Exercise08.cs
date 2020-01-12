using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题8
    /// 创建一个货币类Currency
    /// 在类中声明一个有关金额的静态成员变量,
    /// 构建一个货币转换的静态方法Convert（）
    /// </summary>
    class F1108
    {
        static void Main(string[] args)
        {
            Currency newCurrency = new Currency();
            Currency.Count = 100;
            Currency.Convet();
        }
    }

    class Currency
    {
        public static decimal Count;

        public static void Convet()
        {
            //转换货币
        }
    }
}