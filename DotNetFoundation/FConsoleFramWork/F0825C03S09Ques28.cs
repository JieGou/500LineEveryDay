using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0825C03S09Ques28
{
    class F0825
    {
        /// <summary>
        ///题28
        /// 使用三元运算符,判断成绩是否及格
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("请输出成绩");
            int score = Convert.ToInt32(Console.ReadLine());

            string str = score > 60 ? "及格" : "不及格";
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}