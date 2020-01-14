using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain.LearnCSharpFromShallow.Chapter07
{
    /// <summary>
    /// 第7章 题目05
    /// </summary>
    class F1230
    {
        static void Main(string[] args)
        {
        }
    }

    interface IElectronic
    {
        //接口1
    }

    interface IMobileDevice : IElectronic
    {
        //接口2继承接口1
    }

    class CellPhone : IMobileDevice
    {
    }
}