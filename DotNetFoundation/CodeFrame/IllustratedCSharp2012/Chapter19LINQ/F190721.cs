using System;
using System.Xml.Linq;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * XML : 创建 保存 加载和显示XML文档
     */
    class F19071
    {
        static void Main(string[] args)
        {
            XDocument employees1 =
                new XDocument(
                    new XElement("Emolyees",
                        new XElement("Name", "Bob Smith"),
                        new XElement("Name", "Sally Jones")
                    ));
            employees1.Save("EmployeesFile.xml"); // 保存到文件
            //将保存的文档加载到新变量中
            XDocument employees2 = XDocument.Load("EmployeesFile.xml");
            Console.WriteLine(employees2);
        }
    }
}