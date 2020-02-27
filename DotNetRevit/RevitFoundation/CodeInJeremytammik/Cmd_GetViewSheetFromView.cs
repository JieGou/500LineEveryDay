using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitDevelopmentFoudation.CodeInJeremytammik
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    class Cmd_GetViewSheetFromView : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;

            GetViewSheetFromView(uidoc);

            return Result.Succeeded;
        }

        public void GetViewSheetFromView(UIDocument uidoc)
        {
            Document doc = uidoc.Document;
            string data = "";

            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

            foreach (ElementId selectedId in selectedIds)
            {
                View e = doc.GetElement(selectedId) as View;

                foreach (View v in new FilteredElementCollector(doc).OfClass(typeof(View)).Cast<View>()
                    .Where(q => q.Id.Equals(e.Id)))
                {
                    string thisSheet = "";
                    foreach (ViewSheet viewSheet in new FilteredElementCollector(doc).OfClass(typeof(ViewSheet))
                        .Cast<ViewSheet>())
                    {
                        foreach (ElementId eid in viewSheet.GetAllPlacedViews())
                        {
                            View ev = doc.GetElement(eid) as View;
                            if (ev.Id == v.Id)
                            {
                                thisSheet += viewSheet.SheetNumber + " - " + viewSheet.Name + Environment.NewLine;
                                break;
                            }
                        }
                    }
                    if (thisSheet != "")
                    {
                        data += v.ViewType + " : " + v.Name + " " + Environment.NewLine + thisSheet.TrimEnd(' ', ',') +
                                Environment.NewLine;
                    }

                    else
                    {
                        data += v.ViewType + ": " + v.Name + " " + Environment.NewLine + thisSheet.TrimEnd(' ', ',');
                        data += "NOT ON SHEET" + Environment.NewLine + "\n";
                    }
                }

                TaskDialog.Show("View Report", data);
            }
        }
    }
}