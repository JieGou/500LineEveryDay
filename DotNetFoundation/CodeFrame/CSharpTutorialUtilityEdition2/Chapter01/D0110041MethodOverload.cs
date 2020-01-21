using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.10.3 静态方法和实例方法      
     */
    class D0110041
    {
        static void Main(string[] args)
        {
            UseAbs m = new UseAbs();
        }
    }

    public class UseAbs
    {
        public int Abs(int x)
        {
            return (x < 0 ? -x : x);
        }

        public long Abs(long x)
        {
            return (x < 0 ? -x : x);
        }

        public double Abs(double x)
        {
            return (x < 0 ? -x : x);
        }
    }
}