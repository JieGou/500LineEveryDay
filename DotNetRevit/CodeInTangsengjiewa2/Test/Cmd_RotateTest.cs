using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace CodeInTangsengjiewa2.Test
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_RotateTest : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = doc.ActiveView;

            // var pipe =sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Pipe)).GetElement(doc) as Pipe;
            var wall = sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Wall))
                           .GetElement(doc) as Wall;
            var locationline = (wall.Location as LocationCurve).Curve as Line;
            var startpo = locationline.StartPoint();
            var axisline = Line.CreateUnbound(startpo, XYZ.BasisZ);

            Transaction ts = new Transaction(doc, "rotate");
            ts.Start();
            for (int i = 0; i < 20; i++)
            {
                //这个方法最好,不卡:
                Thread.Sleep(1000);
                ElementTransformUtils.RotateElement(doc, wall.Id, axisline, Math.PI / 4);
                uidoc.RefreshActiveView();

                //// 方法2:该方法达不到效果
                // doc.Regenerate(); // 该方法达不到效果.

                //// 方法3:卡
                // System.Windows.Forms.Application.DoEvents();
                // Thread.Sleep(1000);
            }
            ts.Commit();
            return Result.Succeeded;
        }
    }
}