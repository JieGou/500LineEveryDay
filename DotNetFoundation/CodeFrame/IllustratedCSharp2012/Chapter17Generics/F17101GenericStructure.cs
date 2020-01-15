using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * 泛型结构
     */
    class F17101
    {
        static void Main(string[] args)
        {
            var intData =new PieceOfData<int>(10);
            var stringData =new PieceOfData<string>("Hi,here!");

            Console.WriteLine("intData ={0}",intData.Data);
            Console.WriteLine("stringData ={0}",stringData.Data);
        }
    }

    struct PieceOfData<T>
    {
        private T _data;

        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public PieceOfData(T value)
        {
            _data = value;
        }
    }
}