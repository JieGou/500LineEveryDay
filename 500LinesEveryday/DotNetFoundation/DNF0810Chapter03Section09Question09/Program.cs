using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0810Chapter03Section09Question09
{
    /// <summary>
    /// 创建一个有关部门的枚举类型，部门有销售部，市场部，财务部
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int i = (int)DepartmentEnum.销售部;
            Console.WriteLine(i);
            Console.ReadKey();

            
        }
    }

    public enum DepartmentEnum
    {
        销售部 = 1,
        市场部 = 2,
        财务部 = 3
    }
}