using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FConsoleMain.IllustratedCSharp2012.Chapter19
{
    /*
     * XML : 增加节点
     */
    class F190724
    {
        static void Main(string[] args)
        {
            XDocument xd = new XDocument(
                                         new XElement("root", 
                                                      new XElement("first")
                                                      )
                                         );
            Console.WriteLine("Original tree");
            Console.WriteLine(xd);
            Console.WriteLine("**********");
            XElement rt = xd.Element("root");
            rt.Add(new XElement("Second"));

            rt.Add(new XElement("third"),
                   new XComment("Important Comment"),
                   new XElement("Fourth")
                   );
            Console.WriteLine("Modified tree");
            Console.WriteLine(xd);


        }
    }
}