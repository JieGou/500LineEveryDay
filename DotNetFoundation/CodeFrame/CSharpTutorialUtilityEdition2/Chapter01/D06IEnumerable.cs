using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 练习6 : IEnumerable<T> 接口 的练习
     */
    class D06
    {
        static void Main(string[] args)
        {
            TestStreamReaderEnumerable();
            Console.WriteLine("---");
            TestReadingfile();
            Console.ReadKey();
        }

        public static void TestStreamReaderEnumerable()
        {
            // check thie memory bifore the interator is used
            long memoryBefore = GC.GetTotalMemory(true);

            IEnumerable<string> stringFound;

            //open a file with the StreamReaderEnumerable and check for a string
            try
            {
                stringFound = from line in new StreamReaderEnumerable(@"D:\TestDir1\temp.txt")
                    where line.Contains("string to search for")
                    select line;
                Console.WriteLine("Founds: " + stringFound.Count());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"this example requires a file named D:\TestDir1\temp.txt");
                return;
            }

            //check  the memory after the iterator and output it to the console
            long memoryAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Memory Used With Iterator = \t" +
                              string.Format(((memoryAfter - memoryBefore) / 1000).ToString(), "n") + "kb");
        }


        public static void TestReadingfile()
        {
            long memoryBefore = GC.GetTotalMemory(true);
            StreamReader sr;

            try
            {
                sr = File.OpenText(@"D:\TestDir1\temp.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"this example requires a file named D:\TestDir1\temp.txt");
                return;
            }

            //add the file contens to a generic list of strings
            List<string> fileContents = new List<string>();

            while (!sr.EndOfStream)
            {
                fileContents.Add(sr.ReadLine());
            }

            //check for the string
            var stringFound = from line in fileContents
                where line.Contains("string to search for")
                select line;
            sr.Close();
            Console.WriteLine("Found:" + stringFound.Count());

            // check the memory after when the iterator is not used,and output it to console.
            long memoryAfter = GC.GetTotalMemory(false);
            Console.WriteLine("Memory Used Without Itetator = \t" +
                              string.Format(((memoryAfter - memoryBefore) / 1000).ToString(), "n") + "kb");
        }
    }

    // a custom class that implements IEnumerable(T)
    //when you implement IEnumerable(T)
    // you must also implement IEnumerable and IEnumerator(T)
    public class StreamReaderEnumerable : IEnumerable<string>
    {
        private string _filePath;

        public StreamReaderEnumerable(string filePath)
        {
            _filePath = filePath;
        }

        //必须实现 GetEnumerator(),它将会返回一个新的 StreamReaderEnumerator.
        public IEnumerator<string> GetEnumerator()
        {
            return new StreamReaderEnumerator(_filePath);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class StreamReaderEnumerator : IEnumerator<string>
    {
        private StreamReader _sr;

        public StreamReaderEnumerator(string filePath)
        {
            _sr = new StreamReader(filePath);
        }

        public string _current;

        public string Current
        {
            get
            {
                if (_sr == null || _current == null)
                {
                    throw new InvalidOperationException();
                }

                return _current;
            }
        }

        private object Current1
        {
            get { return this.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current1; }
        }

        //  实现 MoveNext() 和Reset();它们是 Enumerator需要的.
        public bool MoveNext()
        {
            _current = _sr.ReadLine();

            if (_current == null)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            _sr.DiscardBufferedData();
            _sr.BaseStream.Seek(0, SeekOrigin.Begin);
            _current = null;
        }

        //实现IDisposable,
        private bool disposedValue = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }

                _current = null;

                if (_sr != null)
                {
                    _sr.Close();
                    _sr.Dispose();
                }
            }

            this.disposedValue = true;
        }

        ~StreamReaderEnumerator()
        {
            Dispose(false);
        }
    }
}