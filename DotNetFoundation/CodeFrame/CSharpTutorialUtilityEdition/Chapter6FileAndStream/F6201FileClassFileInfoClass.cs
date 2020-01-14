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
    /// 6.2.1  File类常用的方法
    /// </summary>
    class F6201
    {
        static void Main(string[] args)
        {
            string path =@"D:\testTextForCSharp.txt";
            FileStream TextFile = File.Open(path,FileMode.Append);
            byte[] info = {(byte) 'h', (byte) 'e', (byte) 'l', (byte) 'l', (byte) 'o'};
            TextFile.Write(info,0,info.Length);
            TextFile.Close();

            //文件创建方法 : File.Create
            string path2 = @"D:\testTextForCSharp2.txt";
            FileStream newText = File.Create(path2);
            newText.Close();
            Console.WriteLine("创建了{0}",path2);

            // 文件删除方法:File.Delete
            File.Delete(path2);
            Console.WriteLine("删除了{0}",path2);

            //文件的复制方法: File.Copy
            File.Copy(path,path2,true);

            //文件的移动方法: File.Move
            //注意只能在一个逻辑盘下进行文件移动.
            
            //设置文件属性方法: File.SetAttributes
            //pulic static void SetAttributes(string path,FileAttributes fileAttributes)
            // File.SetAttributes(path,FileAttributes.ReadOnly|FileAttributes.Hidden); //竖线| 在这里是并且的意思.

            //判断文件是否存在的方法
             bool exsit = File.Exists(path);
             Console.WriteLine(exsit.ToString());

             //得到文件的属性
             FileInfo fileInfo =new FileInfo(path);
             string s = fileInfo.FullName + "文件长度" + fileInfo.Length + "\n建立时间" + fileInfo.CreationTime + ";";
             Console.WriteLine(s);

        }
    }
   
}