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
     * 使用IEnumerable和IEnumerator的示例
     */
    class F18011
    {
        static void Main(string[] args)
        {
            Spectrum spectrum = new Spectrum();

            foreach (string color in spectrum)
            {
                Console.WriteLine(color);
            }
        }
    }

    class ColorEnumerator : IEnumerator
    {
        internal string[] _colors;
        internal int _position = -1;

        public ColorEnumerator(string[] theColors)
        {
            _colors = new string[theColors.Length];

            for (int i = 0; i < theColors.Length; i++)
            {
                _colors[i] = theColors[i];
            }
        }

        //实现Current
        public object Current
        {
            get
            {
                if (_position == -1) throw new InvalidOperationException();
                if (_position >= _colors.Length) throw new InvalidOperationException();

                return _colors[_position];
            }
        }

        // 实现MoveNext
        public bool MoveNext()
        {
            if (_position < _colors.Length - 1)
            {
                _position++;
                return true;
            }
            else
            {
                return false;
            }
        }
        //实现Reset
        public void Reset()
        {
            _position = -1;
        }
    }

    class Spectrum : IEnumerable
    {
        string[] Colors = { "violet", "blue", "cyan", "green", "yellow", "orange", "red" };

        public IEnumerator GetEnumerator()
        {
            return new ColorEnumerator(Colors);
        }
    }
}