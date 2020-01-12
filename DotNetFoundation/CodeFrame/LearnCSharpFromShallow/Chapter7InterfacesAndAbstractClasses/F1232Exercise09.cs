using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain32
{
    /// <summary>
    /// 第7章 题目09
    /// </summary>
    class F1232
    {
        static void Main(string[] args)
        {
        }
    }


    abstract class Electronic
    {
    }

    abstract class MobileDevice:Electronic
    {
        public abstract void Call();
    }
}