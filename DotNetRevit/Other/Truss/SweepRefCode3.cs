using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using Autodesk.Revit.Creation;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Document = Autodesk.Revit.DB.Document;

namespace Truss
{
    /// <summary>
    /// code in autodesk develop foundation  code 7-17
    /// </summary>
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class SweepRefCode3 : IExternalCommand
    {
        // private Document doc;
        private double length;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            // UIApplication uiApp = commandData.Application;
            // Application app = uiApp.Application;

            Transaction ts = new Transaction(doc, "******");
            ts.Start();

            //定义桁架钢筋长度
            length = 2000 / 304.8;
            //创建桁架上弦钢筋
            CreateUpSteel(doc, length);
            CreateDownSteel(doc, length);

            ts.Commit();

            return Result.Succeeded;
        }

        private void CreateUpSteel(Autodesk.Revit.DB.Document doc, double length)
        {
            CurveArrArray arrarr = new CurveArrArray();
            CurveArray arr = new CurveArray();

            //创建路径平面: CreateSketchPlane  参见Extrusion的例子
            Autodesk.Revit.DB.XYZ normal = XYZ.BasisZ;
            SketchPlane sketchPlane = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, XYZ.Zero));

            //创建用于放样的轮廓,这里创建轮廓线在生成轮廓的方式
            XYZ pnt1 = new XYZ(0, 0, 0);

            double UpSteelDiameter = 8 / 304.8;
            arr.Append(Arc.Create(pnt1, UpSteelDiameter / 2, 0.0d, 360.0d, XYZ.BasisX, XYZ.BasisY));
            arrarr.Append(arr);
            SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);

            //创建用于放样的路径,该路径包括1条线段.
            length = this.length;
            XYZ pnt2 = new XYZ(pnt1.Y + length, 0, 0);

            Curve curve1 = Line.CreateBound(pnt1, pnt2);
            CurveArray curvesPath = new CurveArray();
            curvesPath.Append(curve1);

            //利用轮廓和路径创建放样,轮廓线位于路径的第二条线段的中心点
            Sweep sweep1 = doc.FamilyCreate.NewSweep(true, curvesPath, sketchPlane, profile, 0,
                                                     ProfilePlaneLocation.Start);

            XYZ transAxis = new XYZ(0, 0, 76 / 304.8);
            ElementTransformUtils.MoveElement(doc, sweep1.Id, transAxis);
        }

        private void CreateDownSteel(Autodesk.Revit.DB.Document doc, double length)
        {
            CurveArrArray arrarr = new CurveArrArray();
            CurveArray arr = new CurveArray();

            //创建路径工作平面: CreateSketchPlane  参见Extrusion的例子
            Autodesk.Revit.DB.XYZ normal = XYZ.BasisZ;
            SketchPlane sketchPlane = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, XYZ.Zero));

            //创建用于放样的轮廓,这里创建轮廓线在生成轮廓的方式
            XYZ pnt1 = new XYZ(0, 0, 0);

            double DownSteelDiameter = 6 / 304.8;

            arr.Append(Arc.Create(pnt1, DownSteelDiameter / 2, 0.0d, 360.0d, XYZ.BasisX, XYZ.BasisY));
            arrarr.Append(arr);
            SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);

            //创建用于放样的路径,该路径包括1条线段.
            length = this.length;
            XYZ pnt2 = new XYZ(pnt1.Y + length, 0, 0);

            Curve curve1 = Line.CreateBound(pnt1, pnt2);
            CurveArray curvesPath = new CurveArray();
            curvesPath.Append(curve1);

            //利用轮廓和路径创建放样,轮廓线位于路径的第二条线段的中心点
            Sweep sweep1 = doc.FamilyCreate.NewSweep(true, curvesPath, sketchPlane, profile, 0,
                                                     ProfilePlaneLocation.Start);
            //定义桁架宽度
            double TrusWidth = 80 / 304.8;
            XYZ transAxis1 = new XYZ(0, TrusWidth / 2 - DownSteelDiameter/2, DownSteelDiameter/2);
            var sweep2 = ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis1);

            XYZ transAxis2 = new XYZ(0, -transAxis1.Y, transAxis1.Z);
            var sweep3 = ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis2);

            doc.Delete(sweep1.Id);
        }
    }

    // private void CreateSweep(Autodesk.Revit.DB.Document doc)
    // {
    // CurveArrArray arrarr = new CurveArrArray();
    // CurveArray arr = new CurveArray();
    //
    // //创建路径平面: CreateSketchPlane  参见Extrusion的例子
    // Autodesk.Revit.DB.XYZ normal = XYZ.BasisZ;
    // SketchPlane sketchPlane = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, XYZ.Zero));
    //
    // //创建用于放样的轮廓,这里创建轮廓线在生成轮廓的方式
    // XYZ pnt1 = new XYZ(0, 0, 0);
    // XYZ pnt2 = new XYZ(2, 0, 0);
    // XYZ pnt3 = new XYZ(1, 1, 0);
    // arr.Append(Arc.Create(pnt2, 1.0d, 0.0d, 180.0d, XYZ.BasisX, XYZ.BasisY));
    // arr.Append(Arc.Create(pnt1, pnt3, pnt2));
    // arrarr.Append(arr);
    // SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);
    //
    // //创建用于放样的路径,该路劲包括两条线段.
    // XYZ pnt4 = new XYZ(0, 0, 0);
    // XYZ pnt5 = new XYZ(10, 0, 0);
    // XYZ pnt6 = new XYZ(10, 10, 0);
    // Curve curve1 = Line.CreateBound(pnt4, pnt5);
    // Curve curve2 = Line.CreateBound(pnt5, pnt6);
    // CurveArray curvesPath = new CurveArray();
    // curvesPath.Append(curve1);
    // curvesPath.Append(curve2);
    //
    // //利用轮廓和路径创建放样,轮廓线位于路径的第二条线段的中心点
    // Sweep sweep1 = doc.FamilyCreate.NewSweep(true, curvesPath, sketchPlane, profile, 1,
    //                                          ProfilePlaneLocation.MidPoint);
    //
    // Line axis = Line.CreateBound(pnt4, pnt5);
    // ElementTransformUtils.RotateElement(doc, sweep1.Id, axis, Math.PI / 4);
    // }
}