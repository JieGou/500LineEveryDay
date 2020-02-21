using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RevitDevelopmentFoudation;

namespace ExampleForReflection2
{
    /*
     *  尝试获取一个dll文件中说有的Enum
     *  C:\Program Files\Autodesk\Revit 2020\RevitAPI.dll
     *
     * D:\Git\githubRep2\Gitee500LinesEveryday\DotNetRevit\RevitFoundation\bin\Debug\RevitDevelopmentFoudation.dll
     *
     *2020年2月21日18:13:50: 还是不会
     */


    public class T0221GetEnum
    {
        static void Main(string[] args)
        {
            Assembly asmb = Assembly.UnsafeLoadFrom(@"D:\Git\githubRep2\Gitee500LinesEveryday\DotNetFoundation\ExampleForReflection2\bin\Debug\RevitDevelopmentFoudation.dll");

            Type[] type = asmb.GetTypes();

            string[] Names = System.Enum.GetNames(type[0]);

            Console.ReadKey();
        }
    }
}