using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * XML : 使用XML树的值
     */
    class F190723
    {
        static void Main(string[] args)
        {
            XDocument employeeDoc =
                new XDocument(
                              new XElement("Employees",
                                           new XElement("Employee",
                                                        new XElement("Name", "Bob Smith"),
                                                        new XElement("Name", "Bob Smith2"),
                                                        new XElement("PhoneNumber", "400-333-3333")
                                                       ),
                                           new XElement("Employee",
                                                        new XElement("Name", "Sally Jones"),
                                                        new XElement("PhoneNumber", "415-555-5555"),
                                                        new XElement("PhoneNumber", "415-666-6666")
                                                       )
                                          )
                             );
            XElement root = employeeDoc.Element("Employees");
            IEnumerable<XElement> employees = root.Elements();

            foreach (XElement emp in employees)
            {
                //获取第一个名为Name的子XElement: 第一个employee有两个名字,第二个名字不会被获取
                XElement empNameNode = emp.Element("Name");
                Console.WriteLine(empNameNode.Value);
                Console.WriteLine("---------------");
            }

            foreach (XElement emp in employees)
            {
                //获取所有名为PhoneNumber的子元素
                IEnumerable<XElement> empPhones = emp.Elements("PhoneNumber");
                foreach (XElement phone in empPhones)
                {
                    Console.WriteLine($"{phone.Value}");
                }
            }
        }
    }
}