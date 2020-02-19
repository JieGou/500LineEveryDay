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
    public class R0213LinqNameSpaceJoin : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance));

            var groupA = collector.Where(x => x.Name == "1000 x 1000mm");
            var groupB = collector.Where(x => x.Name == "60 x 30 Student");
            var groupC = collector.Where(x => x.Name == "400 x 800mm");

            //join子句
            var numList = from a in groupA
                join b in groupB on a.Category.Name equals b.Category.Name
                where a.Name.Length > 1 && b.Name.Length > 1
                select new {Name1 = a.Name, Name2 = b.Name, sum = a.Name + b.Name};

            foreach (var e in numList)
            {
                info += "\n" + e.Name1;
                info += "\n" + e.Name2;
                info += "\n" + e.sum;
            }

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}