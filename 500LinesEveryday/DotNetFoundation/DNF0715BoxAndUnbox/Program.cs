using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0715BoxAndUnbox
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            System.Object obj = a;  //装箱box ：将值类型转为object类型
            int b = (int)obj;
            //拆箱unbox


        }
    }
}
