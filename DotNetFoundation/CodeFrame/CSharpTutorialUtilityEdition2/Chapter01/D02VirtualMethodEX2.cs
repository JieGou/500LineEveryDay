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
     * 虚方法练习2:员工打开,经理不打开
     */
    class D02
    {
        static void Main(string[] args)
        {
            Emplyee emplyee = new Emplyee();
            Manager manager = new Manager();
            emplyee.Daka();
            manager.Daka();
            Emplyee emplyee2 = manager;
            emplyee2.Daka();
        }
    }


    class Emplyee
    {
        public virtual void Daka()
        {
            Console.WriteLine("员工要打卡");
        }
    }

    class Manager : Emplyee
    {
        public override void Daka()
        {
            Console.WriteLine("经理不打卡.");
        }
    }
}