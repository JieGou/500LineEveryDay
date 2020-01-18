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
     * 虚方法练习1:真的鸭子嘎嘎叫，木头鸭子吱吱叫，橡皮鸭子唧唧叫
     */
    class D01
    {
        static void Main(string[] args)
        {
            RealDuck realDuck = new RealDuck();
            WoodDuck woodDuck = new WoodDuck();
            RubberDuck rubberDuck = new RubberDuck();
            realDuck.Call();
            woodDuck.Call();
            rubberDuck.Call();
            RealDuck[] ducks = new RealDuck[] {realDuck, woodDuck, rubberDuck};

            for (int i = 0; i < ducks.Length; i++)
            {
                ducks[i].Call();
            }
        }
    }

    class RealDuck
    {
        public virtual void Call()
        {
            Console.WriteLine("真鸭子嘎嘎嘎");
        }
    }

    class WoodDuck : RealDuck
    {
        public override void Call()
        {
            Console.WriteLine("木头鸭子吱吱叫");
        }
    }

    class RubberDuck : RealDuck
    {
        public override void Call()
        {
            Console.WriteLine("橡皮鸭子唧唧叫");
        }
    }
}