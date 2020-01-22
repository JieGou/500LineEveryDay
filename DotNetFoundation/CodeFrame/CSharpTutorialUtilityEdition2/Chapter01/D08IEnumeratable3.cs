using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.CSharpTutorialUtilityEdition2.Chapter01
{
    /// <summary>
    /// 尝试自己写一个可以枚举的自定义类
    /// 假设是一个形状类:
    /// </summary>
    class D08
    {
        static void Main(string[] args)
        {
            MyShapeClass newShapeClass = new MyShapeClass();

            foreach (string s in newShapeClass)
            {
                Console.WriteLine(s);
            }
        }
    }

    public class MyShapeClass : IEnumerable<string>
    {
        string[] shapes = {"shape1", "shape2", "shape3", "shape4"};

        public IEnumerator<string> GetEnumerator()
        {
            return new ShapeEnumerator(shapes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ShapeEnumerator : IEnumerator<string>
    {
        private string[] shapeStrings;
        private int _position = -1;

        public ShapeEnumerator(string[] shape)
        {
            shapeStrings = shape;
        }

        public string Current
        {
            get
            {
                if (_position >= shapeStrings.Length)
                {
                    throw new InvalidOperationException();
                }

                return shapeStrings[_position];
            }
        }

        object IEnumerator.Current
        {
            get { return (this.Current); }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool disposeValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposeValue)
            {
                if (disposing)
                {
                }

                shapeStrings[_position] = null;
            }

            this.disposeValue = true;
        }

        public bool MoveNext()
        {
            if (_position <= shapeStrings.Length - 2)
            {
                _position++;
                return true;
            }
            else return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}