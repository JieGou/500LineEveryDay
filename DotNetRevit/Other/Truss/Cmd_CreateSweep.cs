using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = System.Windows.Forms.Application;
using Document = Autodesk.Revit.DB.Document;
using View = Autodesk.Revit.DB.View;

namespace Truss
{
    /// <summary>
    /// 
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CreateSweep : IExternalCommand
    {
        /// <summary>
        // 创建一个放样
        // // <returns></returns>
        public Autodesk.Revit.Creation.FamilyItemFactory m_familyCreator;

        private Document doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            doc = app.ActiveUIDocument.Document;
            Selection sel = app.ActiveUIDocument.Selection;

            Transaction ts = new Transaction(doc, "******");

            try
            {
                ts.Start();

                // CreateExtrusion(m_familyCreator);
                // MakeNewCurve();

                CreateSweep(doc, CreateSketchPlane(new XYZ(0, 0, 1), new XYZ(1000, 1000, 0)));

                TaskDialog.Show("tips", "创建成功");

                ts.Commit();
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


        //=========
        //code from api.chm
        private Sweep CreateSweep(Autodesk.Revit.DB.Document document, SketchPlane sketchPlane)
        {
            Sweep sweep = null;

            // make sure we have a family document
            if (true == document.IsFamilyDocument)
            {
                // Define a profile for the sweep
                CurveArrArray arrarr = new CurveArrArray();
                CurveArray arr = new CurveArray();

                // Create an ovoid profile
                XYZ pnt1 = new XYZ(0, 0, 0);
                XYZ pnt2 = new XYZ(2000, 0, 0);
                XYZ pnt3 = new XYZ(1000, 1000, 0);
                arr.Append(Arc.Create(pnt2, 1.0d, 0.0d, 180.0d, XYZ.BasisX, XYZ.BasisY));
                arr.Append(Arc.Create(pnt1, pnt3, pnt2));
                arrarr.Append(arr);
                SweepProfile profile = document.Application.Create.NewCurveLoopsProfile(arrarr);

                // Create a path for the sweep
                XYZ pnt4 = new XYZ(1000, 1000, 0);
                XYZ pnt5 = new XYZ(0, 1000, 0);
                Curve curve = Line.CreateBound(pnt4, pnt5);

                CurveArray curves = new CurveArray();
                curves.Append(curve);

                // create a solid ovoid sweep
                sweep = document.FamilyCreate.NewSweep(true, curves, sketchPlane, profile, 0,
                                                       ProfilePlaneLocation.Start);

                // if (null != sweep)
                // {
                //     // move to proper place
                //     XYZ transPoint1 = new XYZ(11, 0, 0);
                //     ElementTransformUtils.MoveElement(document, sweep.Id, transPoint1);
                // }
                // else
                // {
                //     throw new Exception("Failed to create a new Sweep.");
                // }
            }
            else
            {
                throw new Exception("Please open a Family document before invoking this command.");
            }

            return sweep;
        }
        //=========


        private void MakeNewCurve()
        {
            CreateCurve1(new XYZ(200, 100, 10), new XYZ(-120, -50, 50), new XYZ(-1, 0, 0), new XYZ(0, 1, 0));
        }

        private void CreateCurve1(XYZ startPoint, XYZ endPoint, XYZ normal1, XYZ normal2)
        {
            XYZ StartToEnd = new XYZ((endPoint - startPoint).X, (endPoint - startPoint).Y, 0);
            XYZ p_normal1 = new XYZ(normal1.X, normal1.Y, 0);
            XYZ p_normal2 = new XYZ(normal2.X, normal2.Y, 0);

            p_normal1 = p_normal1 / (Math.Sqrt(p_normal1.X * p_normal1.X + p_normal1.Y * p_normal1.Y));
            p_normal2 = p_normal2 / (Math.Sqrt(p_normal2.X * p_normal2.X + p_normal2.Y * p_normal2.Y));

            XYZ XoYprj_start = new XYZ(startPoint.X, startPoint.Y, 0);
            XYZ XoYprj_end = new XYZ(endPoint.X, endPoint.Y, 0);
            //在起点,终点间插值, 并在z=0平面绘制 NurbSpline 曲线

            double[] doubleArray = {1, 1, 1, 1, 1, 1};
            IList<XYZ> controlPoint2 = new List<XYZ>();

            controlPoint2.Add(XoYprj_start);
            controlPoint2.Add(XoYprj_start + p_normal1 * mmToFeet(2000));
            controlPoint2.Add(startPoint + p_normal1 * mmToFeet(4000));
            controlPoint2.Add(endPoint + p_normal2 * mmToFeet(4000));
            controlPoint2.Add(endPoint + p_normal2 * mmToFeet(2000));

            controlPoint2.Add(endPoint);

            Curve nbLine = NurbSpline.CreateCurve(controlPoint2, doubleArray);

            //提取曲线上的拟合点
            IList<XYZ> ptsOncurve = nbLine.Tessellate();

            int ptCount = ptsOncurve.Count;
            ReferencePointArray ptArr = new ReferencePointArray();

            for (int i = 0; i < ptCount; i++)
            {
                XYZ pt = ptsOncurve[i];
                ReferencePoint p =
                    m_familyCreator.NewReferencePoint(new XYZ(pt.X, pt.Y,
                                                              startPoint.Z + i / (ptCount - 1) *
                                                              (endPoint.Z - startPoint.Z)));
                ptArr.Append(p);
            }

            CurveByPoints curve = m_familyCreator.NewCurveByPoints(ptArr);
            curve.Visible = true;

            //创建放样平面并加入参照数组中
            int step = 16; //取4分点进行拟合
            ReferenceArrayArray refArr = new ReferenceArrayArray();

            for (int i = 0; i <= step; i++)
            {
                int position = i * (ptCount - 1) / step;

                if (i == 0)
                {
                    refArr.Append(CreatePlaneByPoint(ptArr.get_Item(position), normal1));
                }
                else if (i == ptArr.Size - 1)
                {
                    refArr.Append(CreatePlaneByPoint(ptArr.get_Item(position), -normal2));
                }
                else
                    refArr.Append(CreatePlaneByPoint(ptArr.get_Item(position),
                                                     (curve.GeometryCurve as HermiteSpline).Tangents[position]));
            }

            //创建放样实体
            m_familyCreator.NewLoftForm(true, refArr);
        }

        //根据参照点和法向量创建放样截面
        private ReferenceArray CreatePlaneByPoint(ReferencePoint refPt, XYZ normal)
        {
            Plane plane = Plane.CreateByNormalAndOrigin(normal, refPt.Position);
            Arc circle = Arc.Create(plane, mmToFeet(300), 0, 2 * Math.PI);
            ModelCurve modelCurve = m_familyCreator.NewModelCurve(circle, SketchPlane.Create(doc, plane));
            ReferenceArray ra = new ReferenceArray();
            ra.Append(modelCurve.GeometryCurve.Reference);
            return ra;
        }

        double mmToFeet(double mmVal)
        {
            return mmVal / 304.8;
        }


        //创建工作平面的函数, 输入为平面的原点和法向量
        internal SketchPlane CreateSketchPlane(Autodesk.Revit.DB.XYZ normal, Autodesk.Revit.DB.XYZ origin)
        {
            //首先创建几何平面
            Plane geometryPlan = Plane.CreateByNormalAndOrigin(normal, origin);

            if (null == geometryPlan)
            {
                return null;
            }

            //根据几何平面创建工作平面
            SketchPlane sketchPlane = SketchPlane.Create(doc, geometryPlan);

            if (null == sketchPlane)
            {
                return null;
            }

            return sketchPlane;
        }

        //创建用于拉伸的轮廓线
        private CurveArrArray CreateExtrusionProfile()
        {
            //轮廓线可以包括一个或者多个关闭的轮廓,所以最后返回是CurveArrArray
            CurveArrArray curveArrArray = new CurveArrArray();
            CurveArray curveArray1 = new CurveArray();

            //创建一个正方体的轮廓线,先创建点,再创建线,最后组合成轮廓
            Autodesk.Revit.DB.XYZ p0 = Autodesk.Revit.DB.XYZ.Zero;
            Autodesk.Revit.DB.XYZ p1 = new Autodesk.Revit.DB.XYZ(10, 0, 0);
            Autodesk.Revit.DB.XYZ p2 = new Autodesk.Revit.DB.XYZ(10, 10, 0);
            Autodesk.Revit.DB.XYZ p3 = new Autodesk.Revit.DB.XYZ(0, 10, 0);
            Line line1 = Line.CreateBound(p0, p1);
            Line line2 = Line.CreateBound(p1, p2);
            Line line3 = Line.CreateBound(p2, p3);
            Line line4 = Line.CreateBound(p3, p0);

            curveArray1.Append(line1);
            curveArray1.Append(line2);
            curveArray1.Append(line3);
            curveArray1.Append(line4);

            curveArrArray.Append(curveArray1);

            return curveArrArray;
        }

        private void CreateExtrusion(FamilyItemFactory familyCreator)
        {
            //调用函数创建拉伸的轮廓线和工作平面
            CurveArrArray curveArrArray = CreateExtrusionProfile();
            SketchPlane sketchPlane = CreateSketchPlane(XYZ.BasisZ, XYZ.Zero);

            //调用API创建拉伸(实心正方体)
            Extrusion recExtrusion = familyCreator.NewExtrusion(true, curveArrArray, sketchPlane, 10);

            //可能会希望把拉伸体移动到希望的位置上
            XYZ transPoint1 = new XYZ(-16, 0, 0);
            ElementTransformUtils.MoveElement(doc, recExtrusion.Id, transPoint1);
        }
    }
}