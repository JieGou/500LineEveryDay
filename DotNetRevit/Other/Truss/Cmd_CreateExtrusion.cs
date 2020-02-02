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
    class Cmd_CreateExtrusion : IExternalCommand
    {
        /// <summary>
        // 代码片段 7-13 创建拉伸体
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

                m_familyCreator = doc.FamilyCreate;

                CreateExtrusion(m_familyCreator);

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