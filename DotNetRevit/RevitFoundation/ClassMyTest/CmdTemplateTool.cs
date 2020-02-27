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
namespace RevitFoundation.ClassMyTest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class CmdTemplateTool2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acView = doc.ActiveView;
            PickedBox pb = sel.PickBox(PickBoxStyle.Directional);
            XYZ min = pb.Min;
            XYZ max = pb.Max;
            double[] xnum = new[] {min.X, max.X};
            double[] ynum = new[] {min.Y, max.Y};
            Level lv1 = acView.GenLevel;
            Level lv2 = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>()
                .Where(m => m.Name == "标高2").First();
            double z = double.NegativeInfinity;
            if (lv2 != null)
            {
                z = lv2.Elevation - lv1.Elevation;
            }
            else
            {
                z = 4000 / 304.8;
            }
            XYZ newMin = new XYZ(xnum.Min(),ynum.Min(),lv1.Elevation);
            XYZ newMax = new XYZ(xnum.Max(),ynum.Max(),z);
            Transform tsf = Transform.Identity;
            tsf.Origin = new XYZ();
            tsf.BasisX = XYZ.BasisX;
            tsf.BasisY = XYZ.BasisY;
            tsf.BasisZ = XYZ.BasisZ;
            BoundingBoxXYZ box =new BoundingBoxXYZ();
            box.Transform = tsf;
            box.Min = newMin;
            box.Max = newMax;
            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();
                //创建三维视图
                View3D v3d = View3D.CreateIsometric(doc,
                    new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>()
                        .Where(m=>m.ViewFamily == ViewFamily.ThreeDimensional).First().Id);
                v3d.SetSectionBox(box);
                ts.Commit();
                uidoc.ActiveView = v3d;
            }
            catch (Exception)
            {
                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }
            return Result.Succeeded;
        }
    }
}