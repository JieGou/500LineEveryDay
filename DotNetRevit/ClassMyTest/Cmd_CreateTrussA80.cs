using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using Autodesk.Revit.Creation;
using Org.BouncyCastle.Security;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Document = Autodesk.Revit.DB.Document;

namespace ClassMyTest
{
    /// <summary>
    /// 完成 创建
    /// </summary>
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class Cmd_CreateTrussA80 : IExternalCommand
    {
        // private Document doc;
        private double length;
        private double UpSteelDiameter;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            // UIApplication uiApp = commandData.Application;
            // Application app = uiApp.Application;

            Transaction ts = new Transaction(doc, "******");
            ts.Start();

            TrussA80 trussR80 = new TrussA80(doc, 450);

            ts.Commit();

            return Result.Succeeded;
        }
    }


    public class TrussA80
    {
        public double Length { set; get; }
        public double BraceSteelDiameter { set; get; }
        public double UpSteelDiameter { set; get; }

        public double Period { set; get; }

        //length参数输出时,单位是mm
        public TrussA80(Document doc, double lengthMM)
        {
            double length = mmToFeet(lengthMM);
            Period = mmToFeet(200);

            // UpSteelDiameter = mmToFeet(8);
            BraceSteelDiameter = mmToFeet(4);
            double TrussBraceTotalHeight = mmToFeet(84);
            double TrussBraceSteelHeight2 = TrussBraceTotalHeight - BraceSteelDiameter;

            XYZ center1 = new XYZ(0, 0, 0);
            XYZ center2 = new XYZ(Period / 2, TrussBraceSteelHeight2 - 5 * BraceSteelDiameter, 0);
            XYZ center3 = new XYZ(Period, 0, 0);

            XYZ BoundPnt1 = new XYZ(center1.X, center1.Y - 2.5 * BraceSteelDiameter, 0);
            XYZ BoundPnt2 = new XYZ(center2.X, center2.Y + 2.5 * BraceSteelDiameter, 0);
            XYZ BoundPnt3 = new XYZ(center3.X, center3.Y - 2.5 * BraceSteelDiameter, 0);

            double InterPnt1X = mmToFeet(7);
            double IntersPnt1Y = -GetYOnCircle(center1, 2.5 * BraceSteelDiameter, InterPnt1X);
            XYZ InterPnt1 = new XYZ(InterPnt1X, IntersPnt1Y, 0);

            double InterPnt2X = mmToFeet(93);
            double IntersPnt2Y = GetYOnCircle(center2, 2.5 * BraceSteelDiameter, InterPnt2X);
            XYZ InterPnt2 = new XYZ(InterPnt2X, IntersPnt2Y, 0);

            double InterPnt3X = mmToFeet(107);
            double IntersPnt3Y = GetYOnCircle(center2, 2.5 * BraceSteelDiameter, InterPnt3X);
            XYZ InterPnt3 = new XYZ(InterPnt3X, IntersPnt3Y, 0);

            double InterPnt4X = mmToFeet(193);
            double IntersPnt4Y = -GetYOnCircle(center3, 2.5 * BraceSteelDiameter, InterPnt4X);
            XYZ InterPnt4 = new XYZ(InterPnt4X, IntersPnt4Y, 0);

            CreateBraceSteel(doc, length, center1, center2, center3, BoundPnt1, BoundPnt2, BoundPnt3, InterPnt1,
                             InterPnt2, InterPnt3, InterPnt4);

            CreateDownSteel(doc, length);
            CreateUpSteel(doc, length);
        }


        private void CreateBraceSteel(
            Autodesk.Revit.DB.Document doc, double pointX, XYZ center1, XYZ center2, XYZ center3, XYZ BoundPnt1,
            XYZ BoundPnt2, XYZ BoundPnt3, XYZ InterPnt1, XYZ InterPnt2, XYZ InterPnt3, XYZ InterPnt4)
        {
            //创建路径的工作平面: CreateSketchPlane  参见Extrusion的例子
            Autodesk.Revit.DB.XYZ normal = XYZ.BasisZ;
            SketchPlane sketchPlane = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, XYZ.Zero));

            //创建用于放样的路径,桁架腹杆钢筋有弧线和直线组成.
            double tempPointX = 0;
            XYZ vector = new XYZ(Period, 0, 0);
            CurveArray curvePath = new CurveArray();

            while (tempPointX < pointX)
            {
                if (BoundPnt1.X <= pointX && pointX <= InterPnt1.X)
                {
                    Curve arc1 = CreateArc1(pointX, BraceSteelDiameter, center1, BoundPnt1);
                    curvePath.Append(arc1);
                    tempPointX = pointX;
                }

                if (InterPnt1.X < pointX && pointX <= InterPnt2.X)
                {
                    Curve arc1 = CreateArc1(InterPnt1.X, BraceSteelDiameter, center1, BoundPnt1);
                    curvePath.Append(arc1);
                    Curve line1 = CreateLine1(pointX, InterPnt1, InterPnt2);
                    curvePath.Append(line1);
                    tempPointX = pointX;
                }

                if (InterPnt2.X < pointX && pointX <= InterPnt3.X)
                {
                    Curve arc1 = CreateArc1(InterPnt1.X, BraceSteelDiameter, center1, BoundPnt1);
                    curvePath.Append(arc1);
                    Curve line1 = CreateLine1(InterPnt2.X, InterPnt1, InterPnt2);
                    curvePath.Append(line1);
                    Curve arc2 = CreateArc2(pointX, BraceSteelDiameter, center2, BoundPnt2, InterPnt2);
                    curvePath.Append(arc2);
                    tempPointX = pointX;
                }

                if (InterPnt3.X < pointX && pointX <= InterPnt4.X)
                {
                    Curve arc1 = CreateArc1(InterPnt1.X, BraceSteelDiameter, center1, BoundPnt1);
                    curvePath.Append(arc1);
                    Curve line1 = CreateLine1(InterPnt2.X, InterPnt1, InterPnt2);
                    curvePath.Append(line1);
                    Curve arc2 = CreateArc2(InterPnt3.X, BraceSteelDiameter, center2, BoundPnt2, InterPnt2);
                    curvePath.Append(arc2);
                    Curve line2 = CreateLine1(pointX, InterPnt3, InterPnt4);
                    curvePath.Append(line2);
                    tempPointX = pointX;
                }

                if (InterPnt4.X < pointX && pointX <= BoundPnt3.X)
                {
                    Curve arc1 = CreateArc1(InterPnt1.X, BraceSteelDiameter, center1, BoundPnt1);
                    curvePath.Append(arc1);
                    Curve line1 = CreateLine1(InterPnt2.X, InterPnt1, InterPnt2);
                    curvePath.Append(line1);
                    Curve arc2 = CreateArc2(InterPnt3.X, BraceSteelDiameter, center2, BoundPnt2, InterPnt2);
                    curvePath.Append(arc2);
                    Curve line2 = CreateLine1(InterPnt4.X, InterPnt3, InterPnt4);
                    curvePath.Append(line2);
                    Curve arc3 = CreateArc3(pointX, BraceSteelDiameter, center3, InterPnt4);
                    curvePath.Append(arc3);
                    tempPointX = pointX;
                }

                if (BoundPnt3.X < pointX)
                {
                    Curve arc1 = CreateArc1(InterPnt1.X, BraceSteelDiameter, center1, BoundPnt1);
                    curvePath.Append(arc1);
                    Curve line1 = CreateLine1(InterPnt2.X, InterPnt1, InterPnt2);
                    curvePath.Append(line1);
                    Curve arc2 = CreateArc2(InterPnt3.X, BraceSteelDiameter, center2, BoundPnt2, InterPnt2);
                    curvePath.Append(arc2);
                    Curve line2 = CreateLine1(InterPnt4.X, InterPnt3, InterPnt4);
                    curvePath.Append(line2);
                    Curve arc3 = CreateArc3(BoundPnt3.X, BraceSteelDiameter, center3, InterPnt4);
                    curvePath.Append(arc3);
                    tempPointX += mmToFeet(Period);

                    center1 = center1.Add(vector);
                    center2 = center2.Add(vector);
                    center3 = center3.Add(vector);
                    BoundPnt1 = BoundPnt1.Add(vector);
                    BoundPnt2 = BoundPnt2.Add(vector);
                    BoundPnt3 = BoundPnt3.Add(vector);
                    InterPnt1 = InterPnt1.Add(vector);
                    InterPnt2 = InterPnt2.Add(vector);
                    InterPnt3 = InterPnt3.Add(vector);
                    InterPnt4 = InterPnt4.Add(vector);
                }
            }

            //创建用于放样的轮廓,这里创建轮廓线再生成轮廓的方式
            XYZ pnt1 = new XYZ(0, 0, 0);
            CurveArrArray arrarr = new CurveArrArray();
            CurveArray arr = new CurveArray();
            arr.Append(Arc.Create(pnt1, BraceSteelDiameter / 2, 0.0d, 360.0d, XYZ.BasisX, XYZ.BasisY));
            arrarr.Append(arr);
            SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);

            //利用轮廓和路径创建放样
            Sweep sweep1 =
                doc.FamilyCreate.NewSweep(true, curvePath, sketchPlane, profile, 0, ProfilePlaneLocation.End);

            // 移动位置.

            Line line = Line.CreateBound(XYZ.Zero, XYZ.BasisX);
            double angle = 70.81;

            XYZ transAxis = new XYZ(0, mmToFeet(-28.6), mmToFeet(11.5));

            ElementTransformUtils.RotateElement(doc, sweep1.Id, line, degreeToRadian(angle));
            ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis);

            ElementTransformUtils.RotateElement(doc, sweep1.Id, line, degreeToRadian(180 - angle - angle));
            XYZ transAxis2 = new XYZ(transAxis.X, -transAxis.Y, transAxis.Z);
            ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis2);

            doc.Delete(sweep1.Id);

            // Plane plane = Plane.CreateByNormalAndOrigin(XYZ.BasisY, XYZ.Zero);
            // ElementTransformUtils.MirrorElement(doc, sweep1.Id, plane);

            // foreach (Curve curve in curvePath)
            // {
            //     ModelCurve modelCurve = doc.FamilyCreate.NewModelCurve(curve, sketchPlane);
            // }
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
            arr.Append(Arc.Create(pnt1, UpSteelDiameter / 2, 0.0d, degreeToRadian(360), XYZ.BasisX, XYZ.BasisY));
            arrarr.Append(arr);
            SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);

            //创建用于放样的路径,该路径包括1条线段.
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

            //创建用于放样的轮廓,这里创建轮廓线再生成轮廓的方式
            XYZ pnt1 = new XYZ(0, 0, 0);
            double DownSteelDiameter = 6 / 304.8;
            arr.Append(Arc.Create(pnt1, DownSteelDiameter / 2, 0.0d, degreeToRadian(360), XYZ.BasisX, XYZ.BasisY));
            arrarr.Append(arr);
            SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);

            //创建用于放样的路径,该路径包括1条线段.
            XYZ pnt2 = new XYZ(pnt1.Y + length, 0, 0);
            Curve curve1 = Line.CreateBound(pnt1, pnt2);
            CurveArray curvesPath = new CurveArray();
            curvesPath.Append(curve1);

            //利用轮廓和路径创建放样,轮廓线位于路径的第二条线段的中心点
            Sweep sweep1 = doc.FamilyCreate.NewSweep(true, curvesPath, sketchPlane, profile, 0,
                                                     ProfilePlaneLocation.Start);

            //定义桁架宽度
            double TrusWidth = 80 / 304.8;
            XYZ transAxis1 = new XYZ(0, TrusWidth / 2 - DownSteelDiameter / 2, DownSteelDiameter / 2);
            var sweep2 = ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis1);
            XYZ transAxis2 = new XYZ(0, -transAxis1.Y, transAxis1.Z);
            var sweep3 = ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis2);
            doc.Delete(sweep1.Id);
        }


        public static double degreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }

        public static double mmToFeet(double mm)
        {
            double feet = mm / 304.8;
            return feet;
        }

        public static double GetYOnCircle(XYZ center, double radius, double x)
        {
            double y = (Math.Sqrt((radius) * (radius) - (x - center.X) * (x - center.X))) + center.Y;
            return y;
        }

        public static double GetYOnLine(XYZ startPoint, XYZ endPoint, double x)
        {
            double y = ((endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X)) * (x - startPoint.X) + startPoint.Y;
            return y;
        }

        private static Curve CreateArc1(double pointX, double BraceSteelDiameter, XYZ center1, XYZ BoundPnt1)
        {
            double arcTerminusponitY =
                -GetYOnCircle(center1, 2.5 * BraceSteelDiameter, pointX);

            XYZ terminusPoint1 = new XYZ(pointX, arcTerminusponitY, 0);

            Line tempLine0 = Line.CreateBound(center1, new XYZ(center1.X + 1, 0, 0));
            Line tempLine1 = Line.CreateBound(center1, BoundPnt1);
            Line tempLine2 = Line.CreateBound(center1, terminusPoint1);

            double startAngle = tempLine0.Direction.AngleTo(tempLine1.Direction);
            double endAngle = tempLine0.Direction.AngleTo(tempLine2.Direction);
            XYZ xAxis = new XYZ(1, 0, 0);
            XYZ yAxis = new XYZ(0, 1, 0);
            Curve curve1 = Arc.Create(center1, BraceSteelDiameter * 2.5, -startAngle, -endAngle, xAxis, yAxis);
            return curve1;
        }

        private static Line CreateLine1(double pointX, XYZ startPoint, XYZ endPoint)
        {
            XYZ targetPoint = new XYZ(pointX, GetYOnLine(startPoint, endPoint, pointX), 0);

            Line line1 = Line.CreateBound(startPoint, targetPoint);
            return line1;
        }

        private static Curve CreateArc2(
            double pointX, double BraceSteelDiameter, XYZ center2, XYZ BoundPoint2, XYZ InterPnt2)
        {
            double arcTerminusponitY = GetYOnCircle(center2, 2.5 * BraceSteelDiameter, pointX);
            XYZ terminusPoint1 = new XYZ(pointX, arcTerminusponitY, 0);
            Line line0 = Line.CreateBound(center2, new XYZ(center2.X + 1, center2.Y, 0));
            Line line1 = Line.CreateBound(center2, InterPnt2);
            Line line2 = Line.CreateBound(center2, terminusPoint1);
            double startAngle = line0.Direction.AngleTo(line2.Direction);
            double endAngle = line0.Direction.AngleTo(line1.Direction);

            // string info = (startAngle).ToString() + "\n" + (endAngle).ToString();
            // TaskDialog.Show("tips", info);

            XYZ xAxis = new XYZ(1, 0, 0);
            XYZ yAxis = new XYZ(0, 1, 0);

            Curve curve1 = Arc.Create(center2, BraceSteelDiameter * 2.5, startAngle, endAngle, xAxis, yAxis);
            return curve1;
        }

        private static Line CreateLine2(double pointX, XYZ startPoint, XYZ endPoint)
        {
            XYZ targetPoint = new XYZ(pointX, GetYOnLine(startPoint, endPoint, pointX), 0);

            Line line1 = Line.CreateBound(startPoint, targetPoint);
            return line1;
        }

        private static Curve CreateArc3(double pointX, double BraceSteelDiameter, XYZ center3, XYZ InterPnt4)
        {
            double arcTerminusponitY = -GetYOnCircle(center3, 2.5 * BraceSteelDiameter, pointX);
            XYZ terminusPoint1 = new XYZ(pointX, arcTerminusponitY, 0);
            Line line0 = Line.CreateBound(center3, new XYZ(center3.X + 1, 0, 0));
            Line line1 = Line.CreateBound(center3, InterPnt4);
            Line line2 = Line.CreateBound(center3, terminusPoint1);

            //angle按顺时针计算
            double startAngle = line0.Direction.AngleTo(line2.Direction);
            double endAngle = line0.Direction.AngleTo(line1.Direction);

            // string info = (startAngle).ToString() + "\n" + (endAngle).ToString();
            // TaskDialog.Show("tips", info);

            XYZ xAxis = new XYZ(1, 0, 0);
            XYZ yAxis = new XYZ(0, 1, 0);
            Curve curve1 = null;
            //curve 按逆时针计算
            curve1 = Arc.Create(center3, BraceSteelDiameter * 2.5, -endAngle, -startAngle, xAxis,
                                yAxis);
            return curve1;
        }
    }
}