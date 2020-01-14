using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain.LearnCSharpFromShallow.Chapter072
{
    /// <summary>
    /// 第7章 题目07 08
    /// </summary>
    class F1231
    {
        static void Main(string[] args)
        {
        }
    }

    abstract class MobileDevice
    {
        public abstract void Call();
    }

    class CellPhone : MobileDevice
    {
        public override void Call()
        {
        }
    }
}