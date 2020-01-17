using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * XML : 使用XML的特性: 增加特性; 查询特性; 移除; 增加或者修改;
     */
    class F190741
    {
        static void Main(string[] args)
        {
            XDocument xd = new XDocument(
                                         new XElement("root",
                                                      new XAttribute("Color", "red"),
                                                      new XAttribute("Size", "large"),
                                                      new XElement("first"),
                                                      new XElement("second")
                                                     )
                                        );


            Console.WriteLine(xd); Console.WriteLine("********");

            XElement rt = xd.Element("root"); //获取元素
            XAttribute color = rt.Attribute("Color"); //获取特性
            XAttribute size = rt.Attribute("Size"); //获取特性

            Console.WriteLine($"color is {color.Value}");
            Console.WriteLine($"size is {size.Value}");

            //移除特性
            rt.Attribute("Color").Remove();
            rt.SetAttributeValue("Size",null);
            Console.WriteLine(xd); Console.WriteLine("********");

            //增加特性,或者修改特性的值
            rt.SetAttributeValue("size","medium");
            rt.SetAttributeValue("width","narrow");
            Console.WriteLine(xd); Console.WriteLine("********");
        }
    }
}