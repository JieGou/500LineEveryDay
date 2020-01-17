using System;
using System.Xml.Linq;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * XML : 创建 保存 加载和显示XML文档
     */
    class F190722
    {
        static void Main(string[] args)
        {
            XDocument employeeDoc =
                new XDocument(
                              new XElement("Employees",
                                           new XElement("Employee",
                                                        new XElement("Name", "Bob Smith"),
                                                        new XElement("PhoneNumber", "400-333-3333")
                                                       ),
                                           new XElement("Employee",
                                                        new XElement("Name", "Sally Jones"),
                                                        new XElement("PhoneNumber", "415-555-5555"),
                                                        new XElement("PhoneNumber", "415-666-6666")
                                                       )
                                          )
                             );
            Console.WriteLine(employeeDoc);
            Console.ReadKey();
        }
    }
}