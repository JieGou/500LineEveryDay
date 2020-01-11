using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0709
{
    /// <summary>
    /// 学生信息- 结构
    ///  </summary>
    public struct Student
    {
        public long SId;
        public string SName;
        public double Score;

    }
    class Program
    {
        static void Main(string[] args)
        {
            //创建结构对象
            Student s= new Student();
           //设置对象的属性： 学号， 姓名，分数
            s.SId = 1;
            s.SName = "张三";
            s.Score = 73;

            //控制台输出
            Console.WriteLine(s.SId.ToString() + s.SName + s.Score.ToString());
            Console.ReadKey();


        }
    }
}
