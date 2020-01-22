using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 练习7 : IEnumerable<T> 接口 的练习2
     *
     * IEnumerator 接口:
     *  实现了IEnumerator接口的枚举器有三个函数成员;Current, MoveNext 和Reset.
     *      Current: 返回当前位置的属性,它是只读属性.
     *      MoveNext:前进位置设置到下一项.如果新的位置是有效的,返回true.否则返回false
     *              并且调用Current之前,先调用MoveNext.因为枚举器的原始位置设置在
     *              序列的第一项之前.
     *      Reset: 把位置设置到原始状态.
     * IEnumerable接口:
     *          实现IEnumerate接口的类,是可枚举类,该接口有一个函数成员
     *          GetEnumerator方法,它返回对象的枚举器
     *      
     */
    class D07
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

    public class Spectrum : IEnumerable
    {
        string[] Colors = {"violet", "blue", "cyan", "green", "yellow", "orange", "", "red"};

        public IEnumerator GetEnumerator()
        {
            return new ColorEnumerator(Colors);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ColorEnumerator : IEnumerator
    {
        private string[] _colors;
        private int _position = -1;

        public ColorEnumerator(string[] theColors)
        {
            _colors = new string[theColors.Length];
            
            for (int i = 0; i < theColors.Length; i++)
            {
                _colors[i] = theColors[i];
            }
            // _colors = theColors;
        }

        public object Current
        {
            get
            {
                if ((_position == null) || (_position >= _colors.Length))
                {
                    throw new InvalidOperationException();
                }

                return _colors[_position];
            }
        }

        public bool MoveNext()
        {
            if (_position <= _colors.Length - 2) 
            {
                _position++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}