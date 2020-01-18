using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTutorialUtilityEdition2.Chapter01
{
    /*
     * 1.3.3 类的实例, 实例又叫做对象.
     */
    class D010303
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("李四", 18);
            //建立一个Person类的对象,并返回对象地址给Person类变量person1
        }
    }
}