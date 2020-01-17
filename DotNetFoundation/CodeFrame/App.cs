using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
            //获取正在执行的程序集
            var asm = Assembly.GetExecutingAssembly();
            //获取此程序集 中定义的类型。
            var types = asm.GetTypes();

            // foreach (var type in types)
            // {
            //     Console.WriteLine(type.Name);
            // }
            //
            // Console.WriteLine("---------------------------------------\n\t");

            string inputstr = "123";

            while (inputstr != "e")
            {
                Console.WriteLine("请输入类名,或者输入e退出:");
                inputstr = Console.ReadLine();

                bool exists = false;

                //判断程序是否存在, 存在直接执行,不存在给出提示
                foreach (var type in types)
                {
                    if (inputstr.ToLower() == type.Name.ToLower())
                    {
                        exists = true;
                    }
                }

                foreach (var type in types)
                {
                    string[] prefix = new string[]
                    {
                        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r",
                        "s", "t", "u", "v", "w", "x", "y", "z"
                    };

                    for (int i = 0; i < prefix.Length; i++)
                    {
                        if ((prefix[i] + inputstr).ToLower() == type.Name.ToLower())
                        {
                            exists = true;
                            inputstr = prefix[i] + inputstr;
                        }
                    }
                }

                //如果存在,直接执行
                if (exists)
                {
                    foreach (var type in types)
                    {
                        if (inputstr.ToLower() == type.Name.ToLower())
                        {
                            //获得该类型下的方法的数组
                            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
                            //按过滤条件过滤数组，得到Main方法
                            var targetMethod = methods
                                .Where(m => m.IsStatic && m.Name.ToLower().Contains("main")).FirstOrDefault();
                            //输出过滤得到的方法的名字，确认是否为Main方法
                            Console.WriteLine("targetMethod.Name:" + targetMethod.Name);

                            //输出类（程序集中的类型，Assembly =>type）的名字
                            Console.WriteLine("type.FullName:" + type.FullName);
                            Console.WriteLine("Assembly.FullName" + asm.FullName);
                            Console.WriteLine("\n程序结果如下:\n******************************\n");

                            var obj = asm.CreateInstance(type.FullName);

                            if (obj == null)
                            {
                                Console.WriteLine("对象未能创建，将退出程序");
                                Console.ReadKey();
                                return;
                            }

                            //定义被调用的方法需要传递的参数数量
                            var objarr = new object[1];
                            //使用放射调用方法：
                            targetMethod.Invoke(obj, objarr);
                            Console.WriteLine("\n******************************\n");
                        }
                    }
                }
            }
        }
    }
}