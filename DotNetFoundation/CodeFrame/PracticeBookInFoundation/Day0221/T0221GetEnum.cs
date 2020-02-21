using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.PracticeBookInFoundation.Day0216
{
    /*
     *  尝试获取一个dll文件中说有的Enum
     *  C:\Program Files\Autodesk\Revit 2020\RevitAPI.dll
     */


    public class T0221GetEnum
    {
        static void Main(string[] args)
        {
            Assembly asmb = Assembly.LoadFrom(@"C:\Program Files\Autodesk\Revit 2020\RevitAPI.dll");

            Type[] type = asmb.GetTypes();

            string[] Names = System.Enum.GetNames(type[0]);

            foreach (string name in Names)
            {
                Console.WriteLine(name + "\n");
            }

            Console.ReadKey();
        }
    }
}