using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleForReflection2
{
    public class Rectangle
    {
        private double _width;
        private double _length;

        public Rectangle()
        {
            _width = 0;
            _length = 0;
            }

        public Rectangle(double givenWidth, double givenLength)
        {
            _width = givenWidth;
            _length = givenLength;

        }

        public double GetArea()
        {
            return _width * _length;
        }

        public double GetPerimeter()
        {
            return 2 * _width + 2 * _length;
        }
    }
}
