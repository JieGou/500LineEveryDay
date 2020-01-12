using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    ///Override
    /// </summary>
    class F1218
    {
        static void Main(string[] args)
        {
           
        }
    }

    class Gragh3
    {
        public double PI = Math.PI;
        public int r = 12;

       public virtual double GetArea()
        {
            return 0;
        }
     
    }

    class Circle3 : Gragh3
    {
       sealed public override double GetArea()
        {
            return 100;
        }
    }



 
}