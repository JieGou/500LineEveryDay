using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * XML :  使用LINQ to XML的LINQ查询
     */
    class F190761
    {
        static void Main(string[] args)
        {
            XDocument xd = new XDocument(
                                         new XElement("MyElements",
                                                      new XElement("first",
                                                                   new XAttribute("Color", "red"),
                                                                   new XAttribute("Size", "small")
                                                                  ),
                                                      new XElement("second",
                                                                   new XAttribute("Color", "yellow"),
                                                                   new XAttribute("Size", "medium")
                                                                  ),
                                                      new XElement("third",
                                                                   new XAttribute("Color", "blue"),
                                                                   new XAttribute("Size", "large")
                                                                  )
                                                     )
                                        );
            Console.WriteLine(xd);
            xd.Save("SimpleSample.xml");

            XDocument rd = XDocument.Load("SimpleSample.xml");
            XElement rt = xd.Element("MyElements");

            var xyz = from e in rt.Elements()
                where e.Name.ToString().Length == 5
                select e;

            foreach (XElement x in xyz)
            {
                Console.WriteLine(x.Name.ToString());

            }

            foreach (XElement x in xyz)
            {
                Console.WriteLine($"    Name:{x.Name};  color:{x.Attribute("Color").Value}; size: {x.Attribute("Size").Value}");
            }

            
            //第二段代码
            var xyz2 = from e in rt.Elements()
                select new {e.Name, color = e.Attribute("Color")}; //创建匿名构造函数

            foreach (var x in xyz)
            {
                Console.WriteLine(x); //默认格式化
            }

            Console.WriteLine();

            foreach (var x in xyz2)
            {
                Console.WriteLine("{0,-6},color:{1,-7}",x.Name,x.color.Value);
            }


        }
    }
}