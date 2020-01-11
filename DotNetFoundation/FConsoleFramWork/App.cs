using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class App
    {
        static void Main(string[] args)
        {
            ConsoleKey key;

            do
            {
                Console.WriteLine("输入要执行的程序文件名:");
                var inputstr = Console.ReadLine();
                var asm = Assembly.GetExecutingAssembly();
                var types = asm.GetTypes();
                int num = 0;

                foreach (var type in types)
                {
                    Console.WriteLine(type.Name);
                }

                foreach (var type in types)
                {
                    if (inputstr.ToLower() == type.Name.ToLower())
                    {
                        var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);

                        var targetMethod = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                            .Where(m => m.IsStatic && m.Name.ToLower().Contains("main")).FirstOrDefault();
                        Console.WriteLine(targetMethod.Name);

                        var objarr = new object[1];
                        var obj = asm.CreateInstance(type.FullName);

                        Console.WriteLine("type.FullName:" + type.FullName + "\n程序结果如下:\n\n");

                        if (obj == null)
                        {
                            Console.WriteLine("对象未能创建，将退出程序");
                            Console.ReadKey();
                            return;
                        }

                        num++;
                        targetMethod.Invoke(obj, objarr);
                    }
                }

                if (num == 0)
                {
                    Console.WriteLine("输入错误");
                }

                Console.WriteLine("请按任意键重新重新输入,或按esc退出");
                key = Console.ReadKey().Key;

            } while (key != ConsoleKey.Escape);
        }
    }
}