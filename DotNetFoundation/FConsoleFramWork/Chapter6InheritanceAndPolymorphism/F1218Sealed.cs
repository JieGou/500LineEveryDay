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
    class F1217
    {
        static void Main(string[] args)
        {
           
        }
    }

    class Gragh2
    {
        public double PI = Math.PI;
        public int r = 12;

        public virtual double GetArea()
        {
            return 0;
        }
     
    }

    class Circle2 : Gragh2
    {
        public override double GetArea()
        {
            return 100;
        }
    }

    class Rectangle2 : Gragh2
    {
        public override double GetArea()
        {
            return 120;
        }
    }
}