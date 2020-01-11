using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class App
    {
        static void Main(string[] args)
        {
            int i = 200;
            string s1 = "这是一个字符串"; //声明变量
            //输出变量到控制台
            Console.WriteLine("输入要执行的程序文件名:");
            var inputstr = Console.ReadLine();

            //var  parseResult= int.TryParse(inputstr, out int result);


            if (true)
            {
                var asm = Assembly.GetExecutingAssembly();
                var types = asm.GetTypes();

                foreach (var type in types)
                {
                    Console.WriteLine(type.Name);

                    if (type.Name.ToLower().Contains(inputstr.ToLower()))
                    {
                        var methods = type.GetMethods(BindingFlags.NonPublic|BindingFlags.Static);
                          
                        var targetMethod = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Where(m => m.IsStatic && m.Name.ToLower().Contains("main")).FirstOrDefault();
                        Console.WriteLine(targetMethod.Name);

                        var objarr = new object[1]  ;
                        var obj = asm.CreateInstance(type.FullName);

                        Console.WriteLine("type.FullName:" +type.FullName+"\n程序结果如下:\n\n");
                        if (obj == null)
                        {
                            Console.WriteLine("对象未能创建，将退出程序");
                            Console.ReadKey();
                            return;

                        }

                        targetMethod.Invoke(obj, objarr);

                    }

                }
            }
            else
            {
                
            }

            Console.WriteLine(i);
            Console.WriteLine(s1);
            Console.ReadKey();

        }
    }
}
