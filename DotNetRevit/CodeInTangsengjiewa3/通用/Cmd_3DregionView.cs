using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa3.BinLibrary.Extensions;
using CodeInTangsengjiewa3.BinLibrary.Helpers;
using CodeInTangsengjiewa3.CodeOfQian;
using FilteredElementCollector = Autodesk.Revit.DB.FilteredElementCollector;

namespace CodeInTangsengjiewa3.通用
{
    /// <summary>
    /// 局部三维视图 (从当前平面视图,向上4000mm 的高度上生成三维视图)
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_3DregionView : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            var acview = uidoc.ActiveView;
            if (!(acview is ViewPlan))
            {
                MessageBox.Show("请在平面视图使用此命令");
            }
            var collector = new FilteredElementCollector(doc);
            var pickedBox = sel.PickBox(PickBoxStyle.Directional); //
            var min = pickedBox.Min;
            var max = pickedBox.Max;

            Transform transform = Transform.Identity; //transformation: 变形
            transform.Origin = XYZ.Zero;
            transform.BasisX = XYZ.BasisX;
            transform.BasisY = XYZ.BasisY;
            transform.BasisZ = XYZ.BasisZ;

            var boxMin = new XYZ(min.X, min.Y, acview.GenLevel.Elevation);
            var boxMax = new XYZ(max.X, max.Y, acview.GenLevel.Elevation + 4000d.MmToFeet());

            BoundingBoxXYZ box = new BoundingBoxXYZ();
            box.Transform = transform;
            box.Min = boxMin;
            box.Max = boxMax;
            var view = default(View3D);
            doc.Invoke(m =>
            {
                view = Create3DView(doc, pickedBox) as View3D;
                doc.Regenerate();
                if (view == null)
                {
                    MessageBox.Show("view_3d is null");
                }
                view.SetSectionBox(box);
                view.LookupParameter("剖面框").Set(1);
            }, "创建三维视图");
            uidoc.ActiveView = view;
            return Result.Succeeded;
        }

        public View Create3DView(Document doc, PickedBox pickedBox)
        {
            var viewFamilyTypes = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>().Where(m => m.ViewFamily == ViewFamily.ThreeDimensional);
            var viewFamilyTypeId = viewFamilyTypes.First().Id;
            var result = View3D.CreateIsometric(doc, viewFamilyTypeId);
            return result;
        }
    }
}