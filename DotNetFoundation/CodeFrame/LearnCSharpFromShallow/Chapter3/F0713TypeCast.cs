using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class F0713
    {
        static void Main(string[] args)
        {
            int result;
            //int result2;
            long long1 = 1;
            long long2 = 2;
            //result = long1 + long2;
            //类型不对， 无法计算
            result = (int)long1 + (int)long2;
            //result2 =int.Parse(long1.ToString());
            Console.ReadKey();

        }
    }
}
