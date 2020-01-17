using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * XML : 使用XML的特性: 增加特性; 查询特性; 移除; 增加或者修改;
     */
    class F190751
    {
        static void Main(string[] args)
        {
            XDocument xd = new XDocument(
                                         new XDeclaration("1.0", "utf-8", "yes"),
                                         new XComment("This is a comment"),
                                         new XProcessingInstruction("xml-stylesheet",
                                                                    @"href=""stories.css""type =""text/css"""),
                                         new XElement("root",
                                                      new XElement("first"),
                                                      new XElement("second")
                                                     )
                                        );
            Console.WriteLine(xd);
            xd.Save("xd.xml"); // 保存到文件
        }
    }
}