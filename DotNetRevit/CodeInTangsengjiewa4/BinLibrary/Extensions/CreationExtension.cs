using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Autodesk.Revit.DB;
using CodeInTangsengjiewa.BinLibrary.Helpers;
using Document = Autodesk.Revit.DB.Document;

namespace CodeInTangsengjiewa4.BinLibrary.Extensions
{
    public static class CreationExtension
    {
        public static void NewLine_withoutTransaction(this Document doc, Line line)
        {
            var dir = line.Direction;
            var origin = line.Origin;
            var norm = default(XYZ);

            norm = dir.GetRandomNorm();
            var plane = default(Plane);
            plane = Plane.CreateByNormalAndOrigin(norm, origin);
            var sketchPlane = SketchPlane.Create(doc, plane);
            doc.Create.NewModelCurve(line, sketchPlane);
        }

        public static void NewLine(this Document doc, Line line)
        {
            var dir = line.Direction;
            var origin = line.Origin;
            var norm = default(XYZ);
            var plane = default(Plane);
            plane = Plane.CreateByNormalAndOrigin(norm, origin);
            doc.Invoke(m =>
            {
                var sketchPlane = SketchPlane.Create(doc, plane);
                doc.Create.NewModelCurve(line, sketchPlane);
            }, "create new line");
        }

        public static void NewBox(this Document doc, BoundingBoxXYZ box)
        {
            var trans = box.Transform;
            var min = box.Min;
            var max = box.Max;
            var x = max.X - min.X;
            var y = max.Y - min.X;
            var z = max.Z - min.Z;

            //
            var endX = min + x * trans.BasisX;
            var lineX = Line.CreateBound(min, endX);
            var lineX1 = Line.CreateBound(endX, endX + y * trans.BasisY);
            var lineX2 = Line.CreateBound(endX, endX + z * trans.BasisZ);

            var endY = min + y * trans.BasisY;
            var lineY = Line.CreateBound(min, endY);
            var lineY1 = Line.CreateBound(endY, endY + x * trans.BasisX);
            var lineY2 = Line.CreateBound(endY, endY + z * trans.BasisZ);

            var endZ = min + z * trans.BasisZ;
            var lineZ = Line.CreateBound(min, endZ);
            var lineZ1 = Line.CreateBound(endZ, endZ + x * trans.BasisX);
            var lineZ2 = Line.CreateBound(endZ, endZ + y * trans.BasisY);

            var _lineX = Line.CreateBound(max, max - x * trans.BasisX);
            var _lineY = Line.CreateBound(max, max - y * trans.BasisY);
            var _lineZ = Line.CreateBound(max, max - z * trans.BasisZ);

            doc.Invoke(m =>
            {
                doc.NewLine_withoutTransaction(lineX);
                doc.NewLine_withoutTransaction(lineX1);
                doc.NewLine_withoutTransaction(lineX2);
                doc.NewLine_withoutTransaction(lineY);
                doc.NewLine_withoutTransaction(lineY1);
                doc.NewLine_withoutTransaction(lineY2);
                doc.NewLine_withoutTransaction(lineZ);
                doc.NewLine_withoutTransaction(lineZ1);
                doc.NewLine_withoutTransaction(lineZ2);
                doc.NewLine_withoutTransaction(_lineX);
                doc.NewLine_withoutTransaction(_lineY);
                doc.NewLine_withoutTransaction(_lineZ);
            }, "创建包围框");
        }

        public static void NewCoordinate(this Document doc, XYZ po, Transform trs, double dis = 2)
        {
            var lineX = Line.CreateBound(po, po + dis * trs.BasisX);
            var lineY = Line.CreateBound(po, po + dis * trs.BasisY);
            var lineZ = Line.CreateBound(po, po + dis * trs.BasisZ);
            doc.Invoke(m =>
            {
                doc.NewLine_withoutTransaction(lineX);
                doc.NewLine_withoutTransaction(lineY);
                doc.NewLine_withoutTransaction(lineZ);
            }, "创建坐标");
        }
    }
}