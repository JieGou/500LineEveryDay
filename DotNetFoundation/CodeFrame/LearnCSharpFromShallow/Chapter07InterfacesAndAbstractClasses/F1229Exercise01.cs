using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain.LearnCSharpFromShallow.Chapter07
{
    /// <summary>
    /// 第7章 题目01 02 03
    /// 创建一个关于产品的接口
    /// 给接口一个方法
    /// 创建一个类继承接口,实现方法
    /// </summary>
    class F1229
    {
        static void Main(string[] args)
        {
        }
    }

    interface IProduct
    {
        //定义接口
        void CreatProducet();
      
    }

    class Product : IProduct
    {
        void IProduct.CreatProducet()
        {
            //生产产品
        }
    }
}