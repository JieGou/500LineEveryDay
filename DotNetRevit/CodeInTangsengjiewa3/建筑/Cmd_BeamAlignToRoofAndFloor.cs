using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa3.BinLibrary.Extensions;

namespace CodeInTangsengjiewa3.建筑
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_BeamAlignToRoofAndFloor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            var acview = doc.ActiveView;
            var IsAlignTopFace = false;   //根据设置确定
            var IsAlignBottomFace = true; //根据设置确定
            var selectedIds = sel.GetElementIds();

            var selectionCollector = new FilteredElementCollector(doc, selectedIds); //将选择集作为容器
            var beamFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            var roofFilter = new ElementCategoryFilter(BuiltInCategory.OST_Roofs);
            var floorFilter = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            var rampFilter = new ElementCategoryFilter(BuiltInCategory.OST_Ramps);
            var structuralFoundationFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFoundation);

            var beamCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType()
                .WherePasses(beamFilter);
            var roofCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType()
                .WherePasses(roofFilter);
            var floorCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType()
                .WherePasses(floorFilter);
            var rampCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType()
                .WherePasses(rampFilter);
            var strFoundationCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType()
                .WherePasses(structuralFoundationFilter);

            //1 梁随屋面: 将于屋面在同一楼层的梁进行处理,使之紧贴屋面
            //-1 获取无线顶面或地面边界线
            var floorFaces = default(IList<Reference>);
            foreach (Floor floor in floorCollector)
            {
                if (IsAlignBottomFace)
                {
                    floorFaces = HostObjectUtils.GetBottomFaces(floor);
                }
                else if (IsAlignTopFace)
                {
                    floorFaces = HostObjectUtils.GetTopFaces(floor);
                }
                //排除空引用
                floorFaces = floorFaces.Where(m => floor.GetGeometryObjectFromReference(m) as Face != null).ToList();

                //for test

                #region test wether face is null
                ///
                #endregion

                if (floorFaces.Count == 0 || floorFaces == null)
                {
                    continue;
                }
                //用屋面边线切断所有投影相交的梁
                foreach (FamilyInstance beam in beamCollector)
                {
                    ///未写完.
                }
            }
            return Result.Succeeded;
        }

        public List<Curve> GetEdgeCurves(Element ele, List<Reference> faceRefs)
        {
            var result = new List<Curve>();
            foreach (var reference in faceRefs)
            {
                var temface = ele.GetGeometryObjectFromReference(reference) as Face;
                if (temface == null)
                {
                    continue;
                }
                var curveloops = temface.GetEdgesAsCurveLoops();
                foreach (CurveLoop curveloop in curveloops)
                {
                    result.AddRange(curveloop.Cast<Curve>().ToList());
                }
            }
            return result;
        }

        /// <summary>
        /// 构造点在的平面
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public Plane ConstructPlane(XYZ origin, XYZ vector1, XYZ vector2)
        {
            var result = default(Plane);
            result = Plane.CreateByNormalAndOrigin(vector1.CrossProduct(vector2).Normalize(), origin);
            return result;
        }

        /// <summary>
        /// 构造线所在的平面
        /// </summary>
        /// <param name="line"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Plane ConstructPlane(Line line, XYZ vector)
        {
            var result = default(Plane);
            result = Plane.CreateByNormalAndOrigin(line.Direction.CrossProduct(vector).Normalize(), line.Origin);
            return result;
        }

        /// <summary>
        /// 点断梁
        /// </summary>
        /// <param name="beam"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public List<FamilyInstance> CutBeam(FamilyInstance beam, XYZ point)
        {
            var result = new List<FamilyInstance>();
            var doc = beam.Document;
            var locationcurve = beam.Location as LocationCurve;
            var locationline = locationcurve.Curve as Line;
            if (locationline == null)
            {
                return result;
            }
            point = point.ProjectToXLine(locationline);
            if (point.IsOnLine(locationline))
            {
                var start = locationline.StartPoint();
                var end = locationline.EndPoint();
                var line1 = Line.CreateBound(start, point);
                var line2 = Line.CreateBound(point, end);

                (beam.Location as LocationCurve).Curve = line1;
                var copiedBeams = ElementTransformUtils.CopyElement(beam.Document, beam.Id, new XYZ());
                var beam2Id = copiedBeams.First();
                var beam2 = beam2Id.GetElement(doc) as FamilyInstance;

                (beam2.Location as LocationCurve).Curve = line2;

                result.Add(beam);
                result.Add(beam2);
            }
            else
            {
                throw new Exception("point is not on beam, can not cut beam!");
            }
            return result;
        }

        /// <summary>
        /// 平面断梁
        /// </summary>
        /// <param name="beam"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<FamilyInstance> CutBeam(FamilyInstance beam, Plane p)
        {
            var locationCurve = beam.Location as LocationCurve;
            var locationLine = locationCurve.Curve as Line;
            var intersectPo = locationLine.Intersect_cus(p);
            return CutBeam(beam, intersectPo);
        }
    }
}