using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace F0833Throw
{
    /// <summary>
    /// 该范例,使用throw语句抛出异常的例子
    /// 还创建了一个自定义的异常处理类,通过此类精确的捕获异常信息.
    /// </summary>
    /// <param name="args"></param>
    class F0833Throw
    {
        static void Main(string[] args)
        {
            try
            {
                throw new UserEmployeeeException("出现异常信息!"); //抛出异常
            }
            catch (UserEmployeeeException e)
            {
                Console.WriteLine("输出结果为:");
                Console.WriteLine(e.Message,e.InnerException);//输出异常信息
                
            }
            Console.ReadKey();

        }
    }


    /// <summary>
    /// 自定义异常处理类
    /// </summary>
    class UserEmployeeeException : Exception
    {
        private string errorinfo = string.Empty;

        /*
         * 无参数的构造函数
         */
        public UserEmployeeeException()
        {
        }

        /*
         * 带一个参数的构造函数
         */
        public UserEmployeeeException(string message) : base(message)
        {
            errorinfo = message; //设置errorinfo
        }

        /*
         *带两个参数的构造函数
        */
        public UserEmployeeeException(string message, Exception inner) : base(message, inner)
        {
            errorinfo = message;
            inner = null;
        }

    }
}