using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FConsoleMain
{
    class App
    {
        static void Main(string[] args)
        {
            start:

            bool judge = true;

            while (judge)
            {
                Console.WriteLine("输入要执行的程序文件名:");
                var inputstr = Console.ReadLine();
                var asm = Assembly.GetExecutingAssembly();
                var types = asm.GetTypes();

                // foreach (var type in types)
                // {
                //     Console.WriteLine(type.Name);
                // }

                int num = 0;

                //判断程序是否存在, 存在直接执行,不存在给出提示
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

                string userInput = null;

                if (num != 0)
                {
                    break;
                }

                else
                {
                    Console.WriteLine("输入错误, 输入exit突出,或者输入内容继续");
                    userInput = Console.ReadLine();
                }

                string keyWord = "exit";
                judge = (num != 0 || userInput != keyWord);
            }
        }
    }
}