using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Creation;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Document = Autodesk.Revit.DB.Document;

/// <summary>
/// notice:
/// copy from https://github.com/AaronZyLee/RevitCurve.git
/// </summary>
namespace Truss
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Command : IExternalCommand
    {
        private static Autodesk.Revit.ApplicationServices.Application m_application;
        private static Autodesk.Revit.DB.Document m_document;
        private Autodesk.Revit.Creation.Application m_appCreater;
        private FamilyItemFactory m_familyCreator;


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                m_application = commandData.Application.Application;
                m_document = commandData.Application.ActiveUIDocument.Document;

                // if (!m_document.IsFamilyDocument)
                // {
                //     message = "无法在非族类文档中使用";
                //     return Result.Failed;
                // }

                m_appCreater = m_application.Create;
                m_familyCreator = m_document.FamilyCreate;

                Transaction ts = new Transaction(m_document);
                ts.Start("创建新曲线");

                // MakeNewCurve();
                CreateModelLine(m_document);

                // commandData.Application.ActiveUIDocument.Selection.PickPoint();
                ts.Commit();
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return Result.Failed;
            }

            return Result.Succeeded;
        }


        private void CreateModelLine(Autodesk.Revit.DB.Document RevitDoc)
        {
            using (Transaction transaction = new Transaction(RevitDoc))
            {
                transaction.Start("Create Model Line");
                Line geoLine = Line.CreateBound(XYZ.BasisY * 10, XYZ.BasisX * 10);
                SketchPlane modelSketch =
                    SketchPlane.Create(RevitDoc, Plane.CreateByNormalAndOrigin(XYZ.BasisZ, XYZ.Zero));
                ModelCurve modelLine = RevitDoc.Create.NewModelCurve(geoLine, modelSketch);
                transaction.Commit();
            }
        }


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
            curve.Visible = false;

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
            ModelCurve modelCurve = m_familyCreator.NewModelCurve(circle, SketchPlane.Create(m_document, plane));
            ReferenceArray ra = new ReferenceArray();
            ra.Append(modelCurve.GeometryCurve.Reference);
            return ra;
        }

        #region Helper Functions

        //==============
        //helper function: find an element of the given type and the name.
        //you can use this, for example,to find Reference or level with the given name.
        //==============

        Element findElement(Type targetType, string targetName)
        {
            //get the elements of the given type
            FilteredElementCollector collector = new FilteredElementCollector(m_document);
            collector.WherePasses(new ElementClassFilter(targetType));

            //parse the collection for the given name
            //using LINQ query here.
            var targetElems = from element in collector
                where element.Name.Equals(targetName)
                select element;

            List<Element> elems = targetElems.ToList<Element>();

            if (elems.Count > 0)
            {
                //we should have only one with the given name.
                return elems[0];
            }

            //cannot find it
            return null;
        }


        //=============
        //helper function: convert millimeter to feet
        //=============
        double mmToFeet(double mmVal)
        {
            return mmVal / 304.8;
        }

        #endregion
    }
}