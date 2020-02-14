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
    public class R0213LinqNameSpaceInto : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance));

            //Into语句
            var groupC = collector.Where(x => x.Name == "1000 x 1000mm");
            var groupD = collector.Where(x => x.Name == "60 x 30 Student");

            var elementList = from a in groupC
                join b in groupD on a.Category.Name equals b.Category.Name
                    into groupE
                from c in groupE
                select c;

            foreach (var e in elementList)
            {
                info += e.Name +"\n";
            }

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}