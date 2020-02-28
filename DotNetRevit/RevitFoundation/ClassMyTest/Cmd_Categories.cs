using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using CodeInTangsengjiewa2.BinLibrary.Helpers;

namespace RevitFoundation.ClassMyTest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_Categories : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acView = uidoc.ActiveView;

            string cateNames = "";

            var cate = doc.Settings.Categories.ForwardIterator();

            while (cate.MoveNext())
            {
                cateNames += (cate.Current as Category).Name + "\n";
            }

            TaskDialog.Show("tips", cateNames);
            return Result.Succeeded;
        }
    }
}