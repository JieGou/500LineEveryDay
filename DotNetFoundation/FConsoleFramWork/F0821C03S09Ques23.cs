using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0821C03S09Ques23
{
    class F0821
    {/// <summary>
     /// 创建一个有关食品的结构提.
     /// 该食品结构体变量,主要包括产品编号,产品名称,价格,生产日期,及保质期.
     /// 产品编号和产品名称为字符串, 价格是浮点数.生产日期和保质期为日期类型.
     /// 根据食品结构体创建一个咖啡的结构体实例对象:
     /// 生产日期为当前日期;
     /// 产品编号为 QS+生产日期+三位随机数
     /// 保质期为两年
     /// </summary>
     /// <param name="args"></param>
        struct Food
    {
        public string Id;
        public string Name;
        public float Price;
        public DateTime ProduceTime;
        public DateTime DueDate;
    }
        static void Main(string[] args)
        {
            Food coffee = new Food();
            coffee.ProduceTime=DateTime.Now;
            Random newRdn =new Random();
            coffee.Id = "QS"+coffee.ProduceTime.ToString("yyyyMMdd")+newRdn.Next(100,999).ToString();
            coffee.Name = "雀巢";
            coffee.Price = 5.00F;
            coffee.DueDate =DateTime.Now.AddYears(2);

            Console.ReadKey();


        }
    }
}
