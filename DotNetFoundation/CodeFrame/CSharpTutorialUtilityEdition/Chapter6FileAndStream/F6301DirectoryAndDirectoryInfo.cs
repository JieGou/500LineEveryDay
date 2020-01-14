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
    /// 6.3.1  Directory和DirectoryInfo
    /// </summary>
    class F6301
    {
        static void Main(string[] args)
        {
            //目录创建方法:Directory.CreatDirectory
            string path = @"D:\TestDir1";
            string path2 = @"D:\TestDir1\Dir2";
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path2);
            Console.WriteLine("{0}创建了", path2);

            //目录属性设置方法
            DirectoryInfo dirInfo =new DirectoryInfo(path);
            dirInfo.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden;

            //目录删除方法
            Directory.Delete(path2);
            Console.WriteLine("{0}删除了",path2);

            //目录移动方法:
            // File.Move();

            //获取当前目录下的所有子目录
          string[] directorys =  Directory.GetDirectories(path);
          
          //获得所有逻辑盘符
          string[] allDrivers = Directory.GetLogicalDrives();

          foreach (string driver in allDrivers)
          {
              Console.WriteLine(driver);
          }

          //获取当前目下的所有文件的方法
          string[] files = Directory.GetFiles(path);
          foreach (string file in files)
          {
              Console.WriteLine(file);
          }

            //判断目录是否存在
            bool exsitDir = Directory.Exists(path);
            Console.WriteLine(exsitDir.ToString());



        }
    }
   
}