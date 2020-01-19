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
     * 练习4 : out in ref
       In:过程不会改写In的内容
     
       Out和out:传入的值不会被过程所读取,但过程可以写  //侧重传出值.

       ref:传入的值,过程会读,也会写 :侧重传出修改后的 传入的值.

       就象你把布料送到裁缝的一个收料箱(裁缝用这个区别是哪家客户) 
       IN:这块布料,不能动,我取时还要原样(我取时会要不要这块料,是我自己的事,你管不着,但你不能把这块料做任何改变,你只能看这块料的质地、色彩等等,你要想改变这块料,那自已去照这块料的样子复制一个) 
       Out和out:我可能给了你布料,也可能没给，也可能我给你的只是一张纸或一块羊皮,但我希望无论我给或没给,你都会给我一件衣服,并放到收料箱中,至于放不放衣服是你的事 
       ref:这块布料,保证是布料,你可以加工,也可以不加工,但无论你加工或是没加工,都得给我放回收料箱中. 
       in方式的是默认的传递方式，即向函数内部传送值，这里不作讲解
     */
    class D04
    {
        static void Main(string[] args)
        {
            gump doit = new gump();

            double x1 = 600;
            double half1 = 0;
            double squared1 = 0;
            double cubed1 = 0;

            Console.WriteLine("Before method->\nx1={0}", x1);
            Console.WriteLine("half1={0}", half1);
            Console.WriteLine("squared1={0}", squared1);
            Console.WriteLine("cubed1={0}\n", cubed1);

            doit.math_routOut(x1, out half1, out squared1, out cubed1);
            Console.WriteLine("After out->\nx1={0}", x1);
            Console.WriteLine("half1={0}", half1);
            Console.WriteLine("squared1={0}", squared1);
            Console.WriteLine("cubed1={0}\n", cubed1);

            x1 = 600;
            half1 = 0;
            squared1 = 0;
            cubed1 = 0;

            doit.math_routRef(x1, ref half1, ref squared1, ref cubed1);
            Console.WriteLine("After ref>\nx1={0}", x1);
            Console.WriteLine("half1={0}", half1);
            Console.WriteLine("squared1={0}", squared1);
            Console.WriteLine("cubed1={0}\n", cubed1);

            ////会报错,提示是只读变量.
            // doit.math_routIn(x1, in half1, in squared1, in cubed1);
            // Console.WriteLine("After in->\nx1={0}", x1);
            // Console.WriteLine("half1={0}", half1);
            // Console.WriteLine("squared1={0}", squared1);
            // Console.WriteLine("cubed1={0}\n", cubed1);
        }
    }

    class gump
    {
        public void math_routOut(double x, out double half, out double squared, out double cubed)
        {
            half = x / 2;
            squared = x * x;
            cubed = x * x * x;
        }

        public void math_routRef(double x, ref double half, ref double squared, ref double cubed)
        {
            half = x / 2;
            squared = x * x;
            cubed = x * x * x;
        }

        ////会报错,提示是只读变量.
        // public void math_routIn(double x, in double half, in double squared, in double cubed)
        // {
        //     half = x / 2;
        //     squared = x * x;
        //     cubed = x * x * x;
        // }
    }
}