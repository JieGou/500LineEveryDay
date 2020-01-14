using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain.LearnCSharpFromShallow.Chapter18
{/// <summary>
/// LearnCSharpFromShallow(由浅入深学C#)
/// 18.3.2 获取运行时的类型 范例:18-4 
///  </summary>
/*
 *system.Reflection命名空间包含了有关的反射的类.Assembly就是常用的类.
 * 使用Assembly对象的LoadFile()方法可以动态加载.dll文件,
 * 而GetType()方法就可以获取相关的类型信息.
 */

    class F1833
    {
        static void Main(string[] args)
        {
            //创建一个Assembly程序集对象,动态加载类库文件
            Assembly ass = Assembly.LoadFile(@"D:\githubRep2\Gitee500LinesEveryday\DotNetFoundation\ExampleForReflection2\bin\Debug\ExampleForReflection2.dll");
            //获取类型
            Type[] types = ass.GetTypes();

            foreach (Type type in types)
            {
                //结构信心
                ConstructorInfo[] myconstructors = type.GetConstructors();
                //字段信息
                FieldInfo[] myField = type.GetFields();
                //方法信息
                MethodInfo[] myMethodInfos = type.GetMethods();
                //属性信息
                PropertyInfo[] myPropertyInfos = type.GetProperties();

                ShowInfo("获取结构信息", myconstructors);
                ShowInfo("获取字段信息",myField);
                ShowInfo("获取方法信息",myMethodInfos);
                ShowInfo("属性信息",myPropertyInfos);
                Console.ReadKey();



            }
       
        }

        static private void ShowInfo(string title, object[] info)
        {
            Console.WriteLine(title);

            foreach (object obj in info)
            {
                Console.WriteLine(obj.ToString());
            }
        }
    }
}