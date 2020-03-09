using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa3.BinLibrary.Extensions;

namespace CodeInTangsengjiewa3.Test
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_RotateTest : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = doc.ActiveView;

            var wall =
                sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Wall)).GetElement(doc) as Wall;
            var locationLine = ((wall.Location) as LocationCurve).Curve as Line;
            var starPoint = locationLine.StartPoint();
            var axisLine = Line.CreateBound(starPoint, XYZ.BasisZ);

            Transaction ts = new Transaction(doc, "ratate animation");
            ts.Start();
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
                ElementTransformUtils.RotateElement(doc, wall.Id, axisLine, 45d.DegreeToRadius());
                uidoc.RefreshActiveView();

                // //方法3:
                // System.Windows.Forms.Application.DoEvents();
                // Thread.Sleep(10000);
            }
            ts.Commit();
            return Result.Succeeded;
        }
    }
}