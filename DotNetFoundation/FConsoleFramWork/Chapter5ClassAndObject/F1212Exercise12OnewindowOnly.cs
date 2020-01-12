using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain
{
    /// <summary>
    /// 题12
    /// 创建一个类,保证此类仅有一个实例,并提供一个全局访问方式
    /// 用于实现单窗口
    /// </summary>
    class F1212
    {
        static void Main(string[] args)
        {
            SingleWindow.GetInstance();
        }
    }

    public class SingleWindow
    {
        private static SingleWindow m_instance = null;

        private SingleWindow()
        {
            //构造函数
        }

        public static SingleWindow GetInstance() //返回值是类本身
        {
            if (m_instance == null)
            {
                m_instance = new SingleWindow();
                Console.WriteLine("创建了这个类唯一的实例");
            }

            return m_instance;
        }
    }
}