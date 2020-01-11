using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0901C04S06
{
   static class F1003Que17
    {
        /// <summary>
        /// 创建一个关于天气的枚举,
        /// 并使用switch语句根据不同的情况,获取不同的天气状况
        /// </summary>
        /// <param name="arg"></param>
   public      static void run()
        {

            string desc = "";
            Weather weather = Weather.fog;

            switch (weather)
            {
                case Weather.sunshine:
                    desc = "今天是晴天";
                    break;
                case Weather.cloudy:
                    desc = "今天多远";
                    break;
                case Weather.rain:
                    desc = "由于";
                    break;
                case Weather.fog:
                    desc = "有雾气";
                    break;
                default:break;
            }

            Console.WriteLine(desc);
            Console.ReadKey();

        }

        public enum Weather
        {
            sunshine =0,
            cloudy =1,
            rain =2,
            snow =3,
            fog =4
        }
    }
}