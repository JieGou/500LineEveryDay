using System;
using System.Collections;
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
            string inputstr = "123";

            while (inputstr != "e")
            {
                Console.WriteLine("请输入类名,或者输入e退出:");
                inputstr = Console.ReadLine();
                var asm = Assembly.GetExecutingAssembly();
                var types = asm.GetTypes();

                // foreach (var type in types)
                // {
                //     Console.WriteLine(type.Name);
                // }

                bool exists = false;

                //判断程序是否存在, 存在直接执行,不存在给出提示
                foreach (var type in types)
                {
                    if (inputstr.ToLower() == type.Name.ToLower())
                    {
                        exists = true;
                    }
                }

                //如果存在,直接执行
                if (exists)
                {
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

                            Console.WriteLine("type.FullName:" + type.FullName + "\n程序结果如下:\n******************************\n");

                            if (obj == null)
                            {
                                Console.WriteLine("对象未能创建，将退出程序");
                                Console.ReadKey();
                                return;
                            }

                            targetMethod.Invoke(obj, objarr);
                            Console.WriteLine("\n******************************\n");
                        }
                    }
                }
                //如果不存在,给出提示,重新执行
                else
                {
                    Console.WriteLine("程序不存在！");
                    continue;
                }
            }
        }
    }
}