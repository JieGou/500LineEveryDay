using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Plumbing;
namespace TeacherTangClass
{
    public static class DebugUtils
    {
        ///管道延长///
        public static void ExtendPipe(Pipe p)
        {
            Document doc = p.Document;
            UIDocument uidoc = new UIDocument(doc);
            Selection sel = uidoc.Selection;
            XYZ point = sel.PickPoint();
            LocationCurve lc = p.Location as LocationCurve;
            Line l = lc.Curve as Line;
            XYZ endpoint1 = null;
            if (l.GetEndPoint(0).DistanceTo(point) < l.GetEndPoint(1).DistanceTo(point))
            {
                endpoint1 = l.GetEndPoint(1);
            }
            else
            {
                endpoint1 = l.GetEndPoint(0);
            }
            Transaction ts = new Transaction(doc, "延长管线");
            ts.Start();
            lc.Curve = Line.CreateBound(endpoint1, point);
            ts.Commit();
        }
        ///在墙的中点,创建剖面///
        ///
        public static View CreatWallSection(Wall wall)
        {
            Document doc = wall.Document;
            LocationCurve lc = wall.Location as LocationCurve;
            Line l = lc.Curve as Line;
            XYZ midpoint = (l.GetEndPoint(0) + l.GetEndPoint(1)) / 2;
            XYZ lineDir = l.Direction;
            Transform tsf = Transform.Identity;
            tsf.Origin = midpoint;
            tsf.BasisZ = lineDir;
            tsf.BasisY = XYZ.BasisZ;
            tsf.BasisX = -lineDir.CrossProduct(XYZ.BasisZ).Normalize();
            Parameter para = wall.LookupParameter("无连接高度");
            double z = para.AsDouble();
            double width = 400 / 304.8;
            double depth = 300 / 308.4;
            XYZ min = new XYZ(-width / 2, 0, -depth);
            XYZ max = new XYZ(width / 2, z, 0);
            BoundingBoxXYZ box = new BoundingBoxXYZ();
            box.Transform = tsf;
            box.Min = min;
            box.Max = max;
            ViewFamilyType vft = (new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>().Where(m => m.ViewFamily == ViewFamily.Section)).First();
            return ViewSection.CreateSection(doc, vft.Id, box);
        }
        //??
        public static UIView ActiveUiview(this UIDocument uidoc)
        {
            var acview = uidoc.ActiveView;
            var uiviews = uidoc.GetOpenUIViews();
            var acuiview = uiviews.Where(m => acview.Id == m.ViewId).FirstOrDefault();
            return acuiview;
        }
        ///由于老师封装的方法没有, 无法实现.
        //public static void ShowLocation(Element element)
        //{
        //    Document doc = element.Document;
        //    UIDocument uIDocument = new UIDocument(doc);
        //    Location lc = element.Location;
        //    if (lc is LocationCurve)
        //    {
        //        Curve c = (lc as LocationCurve).Curve;
        //        if (c is Line)
        //        {
        //            doc.NewLine(c as Line);
        //        }
        //        if (c is Arc)
        //        {
        //            doc.NewCurve(c); // //老师自己封装的方法
        //        }
        //        if (c is Ellipse)
        //        {
        //            doc.NewCurve();
        //        }
        //        if (c is HermiteSpline)
        //        {
        //            doc.NewCurve();
        //        }
        //        if (c is CylindricalHelix)
        //        {
        //            doc.NewCurve();
        //        }
        //        if (c is CylindricalHelix)
        //        {
        //            doc.NewCurve();
        //        }
        //        if (c is NurbSpline)
        //        {
        //            doc.NewCurve();
        //        }
        //    }
        //    else if (lc is LocationPoint)
        //    {
        //        XYZ point = (lc as LocationPoint).Point;
        //        doc.NewModelLineXYZ(point);  //老师自己封装的方法
        //    }
        //}
        ///绘制包围框
        ///
        public static void DrawSrroundBox(Element element)
        {
            Document doc = element.Document;
            View acview = doc.ActiveView;
            BoundingBoxXYZ box = element.get_BoundingBox(acview);
            doc.Newbox(box);
        }
        ///调整三维视图的角度
        ///
        public static void Modify3DViewDirection(View3D view)
        {
            XYZ eyePosition = new XYZ();
            XYZ upDirection = XYZ.BasisZ;
            XYZ forDirection = XYZ.BasisY;
            ViewOrientation3D orientation = new ViewOrientation3D(eyePosition, upDirection, forDirection);
            view.SetOrientation(orientation);
        }
    }
}
