using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FConsoleMain;

namespace FConsoleMain.LearnCSharpFromShallow.Chapter07
{
    /// <summary>
    /// 第7章 题目11
    /// 模拟购物网站中支付商品环节,选择不同的支付平台在线付款.创建一个抽象类Epay,两个派生类PayPal和AliPay,以及一个关联类,EpayFactor
    /// </summary>
    class F1234
    {
        static void Main(string[] args)
        {
        }
    }

    abstract class Epay
    {
        public abstract void Process();
    }

    class PayPal : Epay
    {
        public override void Process()
        {
            //与paypal平台对接
        }
    }

    class Alipay : Epay
    {
        public override void Process()
        {
            EpayFactory epayFactory =new EpayFactory();
            Epay epay = epayFactory.CreatEpay("PayPal");
            epay.Process();
            //处理支付宝平台
            epay = epayFactory.CreatEpay("AliPay");
            epay.Process();

        }
    }

    class EpayFactory
    {
        public Epay CreatEpay(string epayType)
        {
            Epay epay = null;

            switch (epayType)
            {
                case "Paypal": epay=new PayPal();
                    break;
                case "Alipay": epay = new Alipay();
                    break;
                default:break;
            }

            return epay;
        }
    }
}