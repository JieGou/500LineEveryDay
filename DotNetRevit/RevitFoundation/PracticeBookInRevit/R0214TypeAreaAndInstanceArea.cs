using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Helpers;


namespace RevitDevelopmentFoudation.PracticeBookInRevit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class R0214TypeAreaAndInstanceArea : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.WhereElementIsElementType();

            var groups = collector
                .Where(x => x is HostObjAttributes || x is InsertableObject)
                .GroupBy(x => x.Category.Name);

            //分组嵌套
            foreach (var g in groups)
            {
                info += "\n" + g.Key + "\n";

                var familyNames = g.GroupBy(x => ((ElementType) x).FamilyName);

                foreach (var families in familyNames)
                {
                    info += "-" + families.Key + "\n";

                    foreach (var e in families)
                    {
                        info += "-- " + e.Name + "\n";
                    }
                }
            }

            string path = @"D:\Test\Log\1.txt";
            LogHelper.LogWrite(info, path);

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}