using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _0809Chapter03Section09Question08
{
    /// <summary>
    /// 声明一个关于产品的结构体，其中包括 产品名称，单位，产品的类别
    /// </summary>
    class F0809
    {
        static void Main(string[] args)
        {
            product product1 =new product();
            product1.ProductName = "计算器";
            product1.Unit = "个";
            product1.Catrgory = "办公用品";

            Console.WriteLine(product1.ProductName + product1.Unit + product1.Catrgory);
            Console.ReadKey();

        }

        struct product
            {
            public string ProductName;
            public string Unit;
            public string Catrgory;

        }
    }
}