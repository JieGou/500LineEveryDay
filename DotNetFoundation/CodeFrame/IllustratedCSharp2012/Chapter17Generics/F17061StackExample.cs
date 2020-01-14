using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain.IllustratedCSharp2012.Chapter17
{
    /*
     * IllustratedCSharp2012
     * Chapter17.6.1 泛型类
     * 使用泛型实现栈的示意
     */
    class F17061
    {
        static void Main(string[] args)
        {
            MyStack<int> StackInt = new MyStack<int>();
            MyStack<string> StackString = new MyStack<string>();

            StackInt.Push(3);
            StackInt.Push(4);
            StackInt.Push(9);
            StackInt.Print();

            StackString.Push("this is fun");
            StackString.Push("hi, here!");
            StackString.Print();

        }
    }

    class MyStack<T>
    {
        private T[] StackArray;
        private int StackPoint = 0;

        public void Push(T x)
        {
            if (!IsStackFull)
                StackArray[StackPoint++] = x;

        }

        public T Pop()
        {
            return (!IsStackEmpty)
                ? StackArray[--StackPoint]
                : StackArray[0];
        }

        private const int MaxStack = 10;
        bool IsStackFull { get { return StackPoint >= MaxStack; } }
        bool IsStackEmpty { get { return StackPoint <= 0; } }

        public MyStack()
        {
            StackArray = new T[MaxStack];
        }

        public void Print()
        {
            for (int i = StackPoint - 1; i >= 0; i--)
            {
                Console.WriteLine("Value: {0}", StackArray[i]);
            }
        }

    }
}