using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FConsoleMain.CSharpTutorialUtilityEdition.Chapter6
{
    /// <summary>
    /// CSharpTutorialUtilityEdition(C#教程实用版)
    /// 6.5.1  拆分和合并文件 
    /// </summary>
    class F6501
    {
        static void Main(string[] args)
        {
        }

        void SplitFile(string f1, string f2, int f2Size)
        {
            FileStream inFile = new FileStream(f1, FileMode.OpenOrCreate, FileAccess.Read);
            bool mark = true;
            int i = 0;
            int n = 0;
            byte[] buffer = new byte[f2Size];

            while (mark)
            {
                FileStream outFile = new FileStream(f2 + i.ToString() + ".fsm", FileMode.OpenOrCreate, FileAccess.Read);

                if ((n = inFile.Read(buffer, 0, f2Size)) > 0)
                {
                    outFile.Write(buffer, 0, n);
                    i++;
                }
                else
                {
                    mark = false;
                }
            }
            inFile.Close();
        }

        void MergeFile(string f1, string f2, int f2Num)
        {
            FileStream outFile = new FileStream(f1, FileMode.OpenOrCreate, FileAccess.Write);
            int n;
            long l;

            for (int i = 0; i < f2Num; i++)
            {
                FileStream inFile = new
                    FileStream(f2 + i.ToString() + ".fsm", FileMode.OpenOrCreate, FileAccess.Read);

                l = inFile.Length;
                byte[] buffer =new byte[1];
                n = inFile.Read(buffer, 0, 1);
                outFile.Write(buffer,0,n);
                inFile.Close();
            }
            outFile.Close();
        }
    }
}