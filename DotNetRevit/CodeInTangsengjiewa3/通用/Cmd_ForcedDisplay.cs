using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Exception = System.Exception;

namespace CodeInTangsengjiewa3.通用
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_ForcedDisplay : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Application app = commandData.Application.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            View acView = doc.ActiveView;

            Transaction ts = new Transaction(doc, "*******");
            ts.Start();
            try
            {
                FilteredElementCollector collection = new FilteredElementCollector(doc);
                // collection.WhereElementIsNotElementType();
                var list = new List<ElementId>();
                list = collection.WhereElementIsNotElementType().Select(m => m.Id).ToList();
                MessageBox.Show(list.Count.ToString());
                acView.UnhideElements(list);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }
            ts.Commit();
            return Result.Succeeded;
        }
    }
}