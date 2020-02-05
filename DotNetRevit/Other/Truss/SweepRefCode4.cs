// using System;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
// using System.Linq;
// using System.Windows;
// using System.Windows.Media.Animation;
// using Autodesk.Revit.Creation;
// using Application = Autodesk.Revit.ApplicationServices.Application;
// using Document = Autodesk.Revit.DB.Document;
//
// namespace Truss
// {
//     /// <summary>
//     /// code in autodesk develop foundation  code 7-17
//     /// </summary>
//     [TransactionAttribute(TransactionMode.Manual)]
//     [RegenerationAttribute(RegenerationOption.Manual)]
//     public class SweepRefCode4 : IExternalCommand
//     {
//         // private Document doc;
//         private double length;
//
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             UIDocument uiDoc = commandData.Application.ActiveUIDocument;
//             Document doc = uiDoc.Document;
//             // UIApplication uiApp = commandData.Application;
//             // Application app = uiApp.Application;
//
//             Transaction ts = new Transaction(doc, "******");
//             ts.Start();
//
//             TrussR80 trussR80 = new TrussR80(doc, 198);
//
//             ts.Commit();
//
//             return Result.Succeeded;
//         }
//     }
//
//     public class TrussR801
//     {
//         private double _length;
//         public double Length { set; get; }
//
//         // private double _UpSteelDiameter;
//         // double UpSteelDiameter = mmToFeet(8);
//
//         public static double UpSteelDiameter = mmToFeet(8);
//
//
//         public static double BraceSteelDiameter = mmToFeet(4);
//
//         public static double TrussBraceTotalHeight = mmToFeet(85);
//
//
//         private static double TrussBraceSteelHeight2 = TrussBraceTotalHeight - BraceSteelDiameter;
//
//
//         private static XYZ center1 = new XYZ(0, 0, 0);
//
//         private static XYZ center2 =
//             new XYZ(mmToFeet(100), TrussBraceSteelHeight2 - 5 * BraceSteelDiameter, 0);
//
//         private static XYZ center3 = new XYZ(mmToFeet(200), 0, 0);
//
//         private XYZ BoundPnt1 = new XYZ(center1.X, center1.Y - 2.5 * BraceSteelDiameter, 0);
//         private XYZ BoundPnt2 = new XYZ(center2.X, center2.Y + 2.5 * BraceSteelDiameter, 0);
//         private XYZ BoundPnt3 = new XYZ(center3.X, center3.Y - 2.5 * BraceSteelDiameter, 0);
//
//         private static double InterPnt1X = mmToFeet(7);
//         private static double IntersPnt1Y = -GetYOnCircle(center1, 2.5 * BraceSteelDiameter, InterPnt1X);
//         private XYZ InterPnt1 = new XYZ(InterPnt1X, IntersPnt1Y, 0);
//
//         private static double InterPnt2X = mmToFeet(93);
//         private static double IntersPnt2Y = GetYOnCircle(center2, 2.5 * BraceSteelDiameter, InterPnt2X);
//         private XYZ InterPnt2 = new XYZ(InterPnt2X, IntersPnt2Y, 0);
//
//
//         private static double InterPnt3X = mmToFeet(107);
//         private static double IntersPnt3Y = GetYOnCircle(center2, 2.5 * BraceSteelDiameter, InterPnt3X);
//         private XYZ InterPnt3 = new XYZ(InterPnt3X, IntersPnt3Y, 0);
//
//
//         private static double InterPnt4X = mmToFeet(193);
//         private static double IntersPnt4Y = -GetYOnCircle(center3, 2.5 * BraceSteelDiameter, InterPnt4X);
//         private XYZ InterPnt4 = new XYZ(InterPnt4X, IntersPnt4Y, 0);
//
//
//         //length参数输出时,单位是mm
//         public TrussR80(Document doc, double length)
//         {
//             CreateBraceSteel(doc, length);
//         }
//
//
//         private void CreateBraceSteel(Autodesk.Revit.DB.Document doc, double length2)
//         {
//             double lengthP = mmToFeet(length2 % 200);
//             int PeriodNum = Convert.ToInt32(length2 / 200);
//
//             CurveArrArray arrarr = new CurveArrArray();
//             CurveArray arr = new CurveArray();
//
//             //创建用于放样的轮廓,这里创建轮廓线再生成轮廓的方式
//             XYZ pnt1 = new XYZ(0, 0, 0);
//             arr.Append(Arc.Create(pnt1, BraceSteelDiameter / 2, 0.0d, 360.0d, XYZ.BasisX, XYZ.BasisY));
//             arrarr.Append(arr);
//             SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);
//
//             //创建路径的工作平面: CreateSketchPlane  参见Extrusion的例子
//             Autodesk.Revit.DB.XYZ normal = XYZ.BasisZ;
//             SketchPlane sketchPlane = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, XYZ.Zero));
//
//             //创建用于放样的路径,桁架腹杆钢筋有弧线和直线组成.
//             CurveArray curvesPath = new CurveArray();
//
//             // for (int i = 1; i <= PeriodNum; i++)
//             // {
//             //     if (PeriodNum != 0)
//             //     {
//             //         CurveArray tempCurveArray = CreatePeriodCurve();
//             //     }
//             //
//             //     if (PeriodNum == 0 && lengthP != 0)
//             //     {
//             //        
//             //     }
//             // }
//
//             #region  画1个周期内的曲线
//
//             if (0 < lengthP)
//             {
//                 if (lengthP <= mmToFeet(7))
//                 {
//                     Curve curve1 = CreateArc2(lengthP, BoundPnt1, center1);
//                     curvesPath.Append(curve1);
//                 }
//
//                 else
//                 {
//                     Curve curve1 = CreateArc1(mmToFeet(7));
//                     curvesPath.Append(curve1);
//                 }
//             }
//
//             if (mmToFeet(7) < lengthP)
//             {
//                 if (lengthP <= mmToFeet(93))
//                 {
//                     Line line1 = CreateLine1(lengthP, InterPnt1, InterPnt2);
//                     curvesPath.Append(line1);
//                 }
//                 else
//                 {
//                     Line line1 = CreateLine1(InterPnt2.X, InterPnt1, InterPnt2);
//                     curvesPath.Append(line1);
//                 }
//             }
//
//             if ((mmToFeet(93) < lengthP))
//             {
//                 if (lengthP <= mmToFeet(107))
//                 {
//                     Curve curve2 = CreateArc2(lengthP, BoundPnt2, center2);
//                     curvesPath.Append(curve2);
//                 }
//                 else
//                 {
//                     Curve curve2 = CreateArc2(InterPnt3.X, BoundPnt2, center2);
//                     curvesPath.Append(curve2);
//                 }
//             }
//
//             if (mmToFeet(107) < lengthP)
//             {
//                 if (lengthP <= mmToFeet(193))
//                 {
//                     Line line2 = CreateLine1(lengthP, InterPnt3, InterPnt4);
//                     curvesPath.Append(line2);
//                 }
//                 else
//                 {
//                     Line line2 = CreateLine1(InterPnt4.X, InterPnt3, InterPnt4);
//                     curvesPath.Append(line2);
//                 }
//             }
//
//             if ((mmToFeet(193) < lengthP))
//             {
//                 if (lengthP <= mmToFeet(200))
//                 {
//                     Curve curve3 = CreateArc3(lengthP, BoundPnt3, center3);
//                     curvesPath.Append(curve3);
//                 }
//                 else
//                 {
//                     Curve curve3 = CreateArc3(BoundPnt3.X, BoundPnt3, center3);
//                     curvesPath.Append(curve3);
//                 }
//             }
//
//             #endregion
//
//             // //根据curve1创建模型线
//             // foreach (Curve curve in curvesPath)
//             // {
//             //     ModelCurve modelCurve = doc.FamilyCreate.NewModelCurve(curve, sketchPlane);
//             // }
//
//             //利用轮廓和路径创建放样
//             Sweep sweep1 = doc.FamilyCreate.NewSweep(true, curvesPath, sketchPlane, profile, 0,
//                                                      ProfilePlaneLocation.End);
//         }
//
//         private Curve CreateArc1(double length)
//         {
//             double arcTerminusponitY =
//                 -Math.Sqrt((2.5 * BraceSteelDiameter) * (2.5 * BraceSteelDiameter) - length * length);
//             XYZ terminusPoint1 = new XYZ(length, arcTerminusponitY, 0);
//
//             Line arcLine1 = Line.CreateBound(center1, BoundPnt1);
//             Line line2 = Line.CreateBound(center1, terminusPoint1);
//             Line line0 = Line.CreateBound(center1, new XYZ(1, 0, 0));
//
//             double startAngle = line2.Direction.AngleTo(line0.Direction);
//             double endAngle = arcLine1.Direction.AngleTo(line0.Direction);
//
//             XYZ xAxis = new XYZ(1, 0, 0);
//             XYZ yAxis = new XYZ(0, -1, 0);
//             Curve curve1 = Arc.Create(center1, BraceSteelDiameter * 2.5, startAngle, endAngle, xAxis, yAxis);
//
//             return curve1;
//         }
//
//         private Line CreateLine1(double length, XYZ startPoint, XYZ endPoint)
//         {
//             XYZ targetPoint = new XYZ(length, GetYOnLine(startPoint, endPoint, length), 0);
//             Line line1 = Line.CreateBound(startPoint, targetPoint);
//             return line1;
//         }
//
//         private Curve CreateArc2(double length, XYZ BoundPoint, XYZ center)
//         {
//             BoundPoint = BoundPnt2;
//
//             double arcTerminusponitY = GetYOnCircle(center, 2.5 * BraceSteelDiameter, length);
//
//             XYZ terminusPoint1 = new XYZ(length, arcTerminusponitY, 0);
//
//             Line line0 = Line.CreateBound(center, new XYZ(Convert.ToDouble(center.X + 1), center.Y, 0));
//             Line line1 = Line.CreateBound(center, InterPnt2);
//             Line line2 = Line.CreateBound(center, terminusPoint1);
//
//             double startAngle = line0.Direction.AngleTo(line2.Direction);
//             double endAngle = line0.Direction.AngleTo(line1.Direction);
//
//             // string str = "startangel:" + startAngle.ToString();
//             // str += "\nendangle" + endAngle.ToString();
//             // str += "\n CenterPoint:" + center.X + "\n" + center.Y;
//             // str += "\nboundPoint" + BoundPoint.X + "\n" + BoundPoint.Y;
//             // str += "\n辅助点" + Convert.ToDouble(center.X + 1) + "\n" + center.Y;
//             // TaskDialog.Show("tips", str);
//
//             XYZ xAxis = new XYZ(1, 0, 0);
//             XYZ yAxis = new XYZ(0, 1, 0);
//
//             Curve curve1 = Arc.Create(center, BraceSteelDiameter * 2.5, startAngle, endAngle, xAxis, yAxis);
//             return curve1;
//         }
//
//         private Curve CreateArc3(double length, XYZ BoundPoint, XYZ center)
//         {
//             BoundPoint = BoundPnt3;
//
//             double arcTerminusponitY = -GetYOnCircle(center, 2.5 * BraceSteelDiameter, length);
//
//             XYZ terminusPoint1 = new XYZ(length, arcTerminusponitY, 0);
//
//             Line line0 = Line.CreateBound(center, new XYZ(Convert.ToDouble(center.X + 1), center.Y, 0));
//             Line line1 = Line.CreateBound(center, InterPnt4);
//             Line line2 = Line.CreateBound(center, terminusPoint1);
//
//             double startAngle = line0.Direction.AngleTo(line1.Direction);
//             double endAngle = line0.Direction.AngleTo(line2.Direction);
//
//             // string str = "startangel:" + startAngle.ToString();
//             // str += "\nendangle" + endAngle.ToString();
//             // str += "\n CenterPoint:" + center.X + "\n" + center.Y;
//             // str += "\nboundPoint" + BoundPoint.X + "\n" + BoundPoint.Y;
//             // str += "\n辅助点" + Convert.ToDouble(center.X + 1) + "\n" + center.Y;
//             // TaskDialog.Show("tips", str);
//
//             XYZ xAxis = new XYZ(1, 0, 0);
//             XYZ yAxis = new XYZ(0, 1, 0);
//
//             Curve curve1 = null;
//
//             curve1 = Arc.Create(center, BraceSteelDiameter * 2.5, -startAngle, -endAngle, xAxis, yAxis);
//
//             return curve1;
//         }
//
//         private CurveArray CreatePeriodCurve()
//         {
//             CurveArray curvesPath = null;
//
//             Curve curve1 = CreateArc1(InterPnt1.X);
//             curvesPath.Append(curve1);
//
//             Line line1 = CreateLine1(InterPnt2.X, InterPnt1, InterPnt2);
//             curvesPath.Append(line1);
//
//             Curve curve2 = CreateArc2(InterPnt3.X, BoundPnt2, center2);
//             curvesPath.Append(curve2);
//
//             Line line2 = CreateLine1(InterPnt4.X, InterPnt3, InterPnt4);
//             curvesPath.Append(line2);
//
//             Curve curve3 = CreateArc3(BoundPnt3.X, BoundPnt3, center3);
//             curvesPath.Append(curve3);
//
//             return curvesPath;
//         }
//
//
//         private void CreateUpSteel(Autodesk.Revit.DB.Document doc, double length)
//         {
//             CurveArrArray arrarr = new CurveArrArray();
//             CurveArray arr = new CurveArray();
//
//             //创建路径平面: CreateSketchPlane  参见Extrusion的例子
//             Autodesk.Revit.DB.XYZ normal = XYZ.BasisZ;
//             SketchPlane sketchPlane = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, XYZ.Zero));
//
//             //创建用于放样的轮廓,这里创建轮廓线在生成轮廓的方式
//             XYZ pnt1 = new XYZ(0, 0, 0);
//
//             double UpSteelDiameter = 8 / 304.8;
//             arr.Append(Arc.Create(pnt1, UpSteelDiameter / 2, 0.0d, degreeToRadian(360), XYZ.BasisX, XYZ.BasisY));
//             arrarr.Append(arr);
//             SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);
//
//             //创建用于放样的路径,该路径包括1条线段.
//
//             XYZ pnt2 = new XYZ(pnt1.Y + length, 0, 0);
//
//             Curve curve1 = Line.CreateBound(pnt1, pnt2);
//             CurveArray curvesPath = new CurveArray();
//             curvesPath.Append(curve1);
//
//             //利用轮廓和路径创建放样,轮廓线位于路径的第二条线段的中心点
//             Sweep sweep1 = doc.FamilyCreate.NewSweep(true, curvesPath, sketchPlane, profile, 0,
//                                                      ProfilePlaneLocation.Start);
//
//             XYZ transAxis = new XYZ(0, 0, 76 / 304.8);
//             ElementTransformUtils.MoveElement(doc, sweep1.Id, transAxis);
//         }
//
//         private void CreateDownSteel(Autodesk.Revit.DB.Document doc, double length)
//         {
//             CurveArrArray arrarr = new CurveArrArray();
//             CurveArray arr = new CurveArray();
//
//             //创建路径工作平面: CreateSketchPlane  参见Extrusion的例子
//             Autodesk.Revit.DB.XYZ normal = XYZ.BasisZ;
//             SketchPlane sketchPlane = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, XYZ.Zero));
//
//             //创建用于放样的轮廓,这里创建轮廓线再生成轮廓的方式
//             XYZ pnt1 = new XYZ(0, 0, 0);
//
//             double DownSteelDiameter = 6 / 304.8;
//
//             arr.Append(Arc.Create(pnt1, DownSteelDiameter / 2, 0.0d, degreeToRadian(360), XYZ.BasisX, XYZ.BasisY));
//             arrarr.Append(arr);
//             SweepProfile profile = doc.Application.Create.NewCurveLoopsProfile(arrarr);
//
//             //创建用于放样的路径,该路径包括1条线段.
//
//             XYZ pnt2 = new XYZ(pnt1.Y + length, 0, 0);
//
//             Curve curve1 = Line.CreateBound(pnt1, pnt2);
//             CurveArray curvesPath = new CurveArray();
//             curvesPath.Append(curve1);
//
//             //利用轮廓和路径创建放样,轮廓线位于路径的第二条线段的中心点
//             Sweep sweep1 = doc.FamilyCreate.NewSweep(true, curvesPath, sketchPlane, profile, 0,
//                                                      ProfilePlaneLocation.Start);
//             //定义桁架宽度
//             double TrusWidth = 80 / 304.8;
//             XYZ transAxis1 = new XYZ(0, TrusWidth / 2 - DownSteelDiameter / 2, DownSteelDiameter / 2);
//             var sweep2 = ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis1);
//
//             XYZ transAxis2 = new XYZ(0, -transAxis1.Y, transAxis1.Z);
//             var sweep3 = ElementTransformUtils.CopyElement(doc, sweep1.Id, transAxis2);
//
//             doc.Delete(sweep1.Id);
//         }
//
//         private static double degreeToRadian(double degree)
//         {
//             return degree * Math.PI / 180;
//         }
//
//         private static double mmToFeet(double mm)
//         {
//             double feet = mm / 304.8;
//             return feet;
//         }
//
//         public static double GetYOnCircle(XYZ center, double radius, double x)
//         {
//             double y = (Math.Sqrt((radius) * (radius) - (x - center.X) * (x - center.X))) + center.Y;
//             return y;
//         }
//
//         public static double GetYOnLine(XYZ startPoint, XYZ endPoint, double x)
//         {
//             double y = ((endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X)) * (x - startPoint.X) + startPoint.Y;
//             return y;
//         }
//     }
// }