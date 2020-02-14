using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace RevitDevelopmentFoudation.PracticeBookInRevit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class R0213LinqNameSpaceFrom : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance));

            var groupA = new[] {3, 4, 5, 6, 7};
            var groupB = new[] {4, 5, 6, 7, 8};

            //From子句 创建的时候不执行，创建的是一段可以执行查询功能的代码。
            //from 增加查询的集合
            //let  定义新的查询变量
            //orderby
            var numList =
                from a in groupA
                orderby a descending 
                from b in groupB
                let sum = a + b
                where sum == 12
                where a > 4
                select new {a, b, sum};
            foreach (var i in numList)
            {
                info += i.a.ToString() + "+" + i.b.ToString() + "=" + i.sum.ToString() + "\n";
            }



            //Union
            var groupC = collector.Where(x => x.Name == "1000 x 1000mm");
            var groupD = collector.Where(x => x.Name == "60 x 30 Student");
            var groupE = collector.Where(x => x.Name == "400 x 800mm");
            foreach (var e in groupC.Union(groupD).Union(groupE))
            {
                info += e.Id.IntegerValue + "\n";
            }
            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}