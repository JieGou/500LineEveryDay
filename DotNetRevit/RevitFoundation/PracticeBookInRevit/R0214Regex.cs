using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Helpers;


namespace RevitFoundation.PracticeBookInRevit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class R0214Regex : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Selection sel = commandData.Application.ActiveUIDocument.Selection;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //用正则表达式过滤 
            var elementList = collector
                .OfClass(typeof(FamilyInstance))
                .Where(x => Regex.IsMatch(x.Name, @".*Student"));

            foreach (var e in elementList)
            {
                info += e.Id.IntegerValue + "\n";
            }

            ICollection<ElementId> ids = elementList.Select(x => x.Id).ToList();
            sel.SetElementIds(ids);
            string path = @"D:\Test\Log\1.txt";
            LogHelper.LogWrite(info, path);

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}