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
    public class R0210Linq : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            // foreach (Element e in new FilteredElementCollector(doc)
            //     .OfClass(typeof(FamilyInstance))
            //     .OfCategory(BuiltInCategory.OST_Doors))
            // {
            //     FamilyInstance fi = e as FamilyInstance;
            //     FamilySymbol fs = fi.Symbol;
            //     Family family = fs.Family;
            //     info += family.Name + " : " + fs.Name + " : " + fi.Name + Environment.NewLine;
            // }

            foreach (FamilyInstance fi in new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_Doors)
                .Cast<FamilyInstance>())
            {

                FamilySymbol fs = fi.Symbol;
                Family family = fs.Family;
                info += family.Name + ": " + fs.Name + ": " + fi.Name + Environment.NewLine;
            }

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}