﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace CodeInTangsengjiewa2.CodeInHuanGS
{
    /// <summary>
    /// Cmd_RayMethod再敲一遍
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_RayMethodRE : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            //选择一个点 
            Reference refPoint = sel.PickObject(ObjectType.PointOnElement);
            XYZ point1 = refPoint.GlobalPoint; //The position on which the reference is hit.???

            //射线方向及工作平面法向量
            XYZ rayDirection = XYZ.BasisZ;
            XYZ skVector = XYZ.BasisX; //???????????

            //当选择的主体为平面时,射线根据选择的点与此面的法线方向进行放射
            if (refPoint.ElementReferenceType == ElementReferenceType.REFERENCE_TYPE_SURFACE)
            {
                PlanarFace pFace = null;
                //主体是链接的图元时,获取平面的方法
                if (refPoint.LinkedElementId.IntegerValue != -1)
                {
                    RevitLinkInstance linkIIns = doc.GetElement(refPoint) as RevitLinkInstance;
                    Document linkDoc = linkIIns.GetLinkDocument();
                    Element linkElem = linkDoc.GetElement(refPoint.LinkedElementId);
                    Options opt = new Options();
                    opt.DetailLevel = ViewDetailLevel.Fine;
                    GeometryElement geoElem = linkElem.get_Geometry(opt);
                    pFace = getTarFace(geoElem, point1); //point1是原始点.
                }
                else
                {
                    //判断是否FamilyInstance类型的族,采用不同的获取方法 
                    Element elem = doc.GetElement(refPoint);
                    if (elem is FamilyInstance)
                    {
                        Options opt = new Options();
                        opt.DetailLevel = ViewDetailLevel.Fine;
                        GeometryElement ge = elem.get_Geometry(opt);
                        pFace = getTarFace(ge, point1);
                    }
                    else
                    {
                        pFace = elem.GetGeometryObjectFromReference(refPoint) as PlanarFace;
                    }
                }

                //修正射线方向及工作平面法向量    
                if (pFace != null)
                {
                    rayDirection = pFace.FaceNormal;
                    skVector = pFace.XVector;
                }
            }

            //获得视图
            View3D view3D = doc.ActiveView as View3D;

            //创建射线测量出第二点
            ExclusionFilter filter =
                new ExclusionFilter(new List<ElementId>() {refPoint.ElementId, refPoint.LinkedElementId});

            ReferenceIntersector refIntersector = new ReferenceIntersector(filter, FindReferenceTarget.All, view3D);
            refIntersector.FindReferencesInRevitLinks = true;
            ReferenceWithContext rwc = refIntersector.FindNearest(point1, rayDirection);

            if (rwc != null)
            {
                XYZ point2 = rwc.GetReference().GlobalPoint;
                //创建模型线
                Line line = Line.CreateBound(point1, point2);
                TaskDialog.Show("距离", line.Length.FeetToMm().ToString("0.00"));
                using (Transaction ts = new Transaction(doc, "尺寸"))
                {
                    ts.Start();
                    SketchPlane sk = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(skVector, point1));
                    ModelCurve modelCurve = doc.Create.NewModelCurve(line, sk);
                    sel.SetElementIds(new List<ElementId>(){modelCurve.Id});
                    ts.Commit();
                }
            }
            else
            {
                TaskDialog.Show("返回结果", "未检测到图元");
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// 获得与UV点相交的面
        /// </summary>
        /// <param name="geometryElement"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        PlanarFace getTarFace(GeometryElement geometryElement, XYZ point)
        {
            PlanarFace face = null;
            //遍历几何元素里的几何object
            foreach (GeometryObject geometryObject in geometryElement)
            {
                //将 geometryObject强转为 solid
                Solid solid = geometryObject as Solid;
                //遍历solid的面
                if (solid != null && solid.Faces.Size > 0)
                {
                    foreach (Face f in solid.Faces)
                    {
                        PlanarFace pFace = f as PlanarFace;
                        if (pFace != null)
                        {
                            try
                            {
                                //获取点到面的距离为0的点
                                if (Math.Abs(pFace.Project(point).Distance) < 1e-4)
                                {
                                    face = pFace;
                                    break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
                if (face != null)
                {
                    break;
                }
            }
            //
            if (face == null)
            {
                foreach (GeometryObject geometryObject in geometryElement)
                {
                    GeometryInstance geoIns = geometryObject as GeometryInstance;
                    if (geoIns != null)
                    {
                        face = getTarFace(geoIns.GetInstanceGeometry(), point);
                    }
                }
            }
            return face;
        }
    }
}