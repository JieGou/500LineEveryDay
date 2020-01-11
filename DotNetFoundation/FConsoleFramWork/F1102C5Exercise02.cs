using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    class F1102
    {
        
        static void Main(string[] args)
        {

            Log log = new Log();
            log.LogName = "数据库日志";
            log.LogSize = 4.7778;
            log.ReadLog();
            log.WriteLog();

            Console.ReadKey();



        }
    }
}