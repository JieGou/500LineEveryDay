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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.6.3 f异常语句:
     * try cathch  ;try finally; try catch finally 可以有多个catch语句
     */
    class D010603
    {
        static void Main(string[] args)
        {
            StreamReader sr = null;

            try
            {
                sr = File.OpenText("d:\\test.txt"); //可能产生异常
                string s;

                while (sr.Peek() != -1)
                {
                    s = sr.ReadLine(); //可能产生异常
                    Console.WriteLine(s);
                }
            }
            catch (DirectoryNotFoundException e) // 无指定目录异常
            {
                Console.WriteLine(e.Message);
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine("文件" + e.FileName + "未发现");
            }

            catch (Exception e)
            {
                Console.WriteLine("处理失败:{0}", e.Message);
            }

            finally
            {
                if (sr != null)
                    sr.Close();
            }
            Console.ReadKey();

        }
    }
}