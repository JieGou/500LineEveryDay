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
    public class R0213LinqNameSpaceGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance));

            //group子句
            var groupA =( from e in collector
                group e by e.Category.Name).ToList();

            foreach (var g in groupA)
            {
                info += g.Key + "\n";

                foreach (var e in g)
                {
                    info += "\t" + e.Name + "\n";
                }
            }

           
            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}