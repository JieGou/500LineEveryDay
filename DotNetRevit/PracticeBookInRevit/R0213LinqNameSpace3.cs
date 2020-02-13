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
    public class R0213LinqNameSpace3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementClassFilter filter = new ElementClassFilter(typeof(FamilyInstance));
            collector.WherePasses(filter);

            var groupA = collector.Where(x => x.Name == "1000 x 1000mm");
            var groupB = collector.Where(x => x.Name == "400 x 800mm");
            var groupC = collector.Where(x => x.Name == "60 x 30 Student");

            var numList = from a in groupA
                // from b in groupB
                // from c in groupB
                where a.Name.Length > 1
                select new {Name = a.Name, CateName = a.Category.Name};

            info += "\n" + numList.FirstOrDefault().Name;
            info += "\n" + numList.FirstOrDefault().CateName;

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}