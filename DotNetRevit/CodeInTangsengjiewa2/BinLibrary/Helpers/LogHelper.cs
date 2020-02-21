using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Converters;

namespace CodeInTangsengjiewa2.BinLibrary.Helpers
{
    public static class LogHelper
    {
        public static void LogException(Action action, string path)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                LogWrite(e.ToString(), path);
            }
        }

        public static void LogWrite(string msg, string path, bool appened = false)
        {
            StreamWriter sw = new StreamWriter(path, appened);
            sw.Write(msg);
            sw.Close();
        }
    }
}