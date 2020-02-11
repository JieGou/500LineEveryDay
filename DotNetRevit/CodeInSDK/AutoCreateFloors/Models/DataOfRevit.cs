using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Commands;
using RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Helpers;

namespace RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Models
{
    class DataOfRevit
    {
        private UIDocument uidoc = null;
        private Document doc = null;

        public CurveArrArray curveArrArray = new CurveArrArray();
        public  List<FloorType> FloorTypes { get; set; }
        public static bool result;
        private List<FamilyInstance> structuralBeams =new List<FamilyInstance>();

        public Level currentLevel = null;

        public  DataOfRevit(ExternalCommandData externalCommand)
        {
            uidoc = externalCommand.Application.ActiveUIDocument;
            doc = uidoc.Document;
            FloorTypes = Tools.GetAllFloorTypes(doc);

            GetCurveArrArray();
        }



        public List<Element> GetPickedElements()
        {
            List<Element> pickedElements = new List<Element>();
            try
            {
                List<Reference> refsList = uidoc.Selection.PickObjects(ObjectType.Element, new FilteredColumnAndBeams(), "选择梁和柱").ToList();
                if (refsList.Count == 0)
                {
                    result = false;
                }
                foreach (var refs in refsList)
                {
                    pickedElements.Add(doc.GetElement(refs));
                }
                structuralBeams = pickedElements.Where(x => x.Category.Name == Category.GetCategory(doc, BuiltInCategory.OST_StructuralFraming).Name).Cast<FamilyInstance>().ToList();
                currentLevel = doc.GetElement(structuralBeams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId()) as Level;
            }
            catch (Exception)
            {

                result = false;
            }
            return pickedElements;
        }

        private List<Solid> GetSolids(List<Element> elemsList)
        {
            List<Solid> solidsOfElems = new List<Solid>();
            if (elemsList.Count != 0)
            {
                foreach (Element elem in elemsList)
                {
                    GeometryElement geoList = elem.get_Geometry(new Options());
                    foreach (GeometryObject geo in geoList)
                    {
                        Solid solid = geo as Solid;
                        if (solid != null)
                        {
                            if (solid != null && solid.Volume != 0)
                            {
                                solidsOfElems.Add(solid);
                            }
                        }
                    }
                }
            }
            return solidsOfElems;
        }

        public Solid UnionSolids(List<Solid> solidsList)
        {
            Solid solid = null;
            if (solidsList.Count > 1)
            {
                solid = BooleanOperationsUtils.ExecuteBooleanOperation(solidsList[0], solidsList[1], BooleanOperationsType.Union);
                for (int i = 1; i < solidsList.Count; i++)
                {
                    solid = BooleanOperationsUtils.ExecuteBooleanOperation(solid, solidsList[i], BooleanOperationsType.Union);
                }
            }
            return solid;
        }

        public CurveArrArray GetCurveArrArray(Solid solid)
        {
            double max = 0;
            double elevation = 0;
            foreach (var elem in structuralBeams)
            {
                elevation = elem.get_Parameter(BuiltInParameter.STRUCTURAL_ELEVATION_AT_BOTTOM).AsDouble();
                if (max < elevation)
                {
                    max = elevation;
                }
            }
            XYZ normal = XYZ.BasisZ;
            XYZ origin = new XYZ(0, 0, max - 0.1);
            Plane plane = Plane.CreateByNormalAndOrigin(normal, origin);
            List<CurveLoop> curveLoops = new List<CurveLoop>();
            Solid newSolid = BooleanOperationsUtils.CutWithHalfSpace(solid, plane);
            CurveArrArray curveArrArray = new CurveArrArray();
            CurveArray curveArray = null;
            if (newSolid != null)
            {
                FaceArrayIterator iterator = newSolid.Faces.ForwardIterator();
                while (iterator.MoveNext())
                {
                    PlanarFace planarFace = iterator.Current as PlanarFace;
                    if (planarFace != null)
                    {
                        if (planarFace.FaceNormal.IsAlmostEqualTo(new XYZ(0, 0, 1)))
                        {
                            //删除最大的闭合线
                            curveLoops = planarFace.GetEdgesAsCurveLoops().OrderBy(x => x.GetExactLength()).ToList();
                            int index = curveLoops.IndexOf(curveLoops.Last());
                            curveLoops.RemoveAt(index);
                            break;
                        }
                    }
                }
                if (curveLoops.Count != 0)
                {
                    foreach (CurveLoop curveLoop in curveLoops)
                    {
                        CurveLoopIterator loopIterator = curveLoop.GetCurveLoopIterator();
                        curveArray = new CurveArray();
                        while (loopIterator.MoveNext())
                        {
                            curveArray.Append(loopIterator.Current);
                        }
                        curveArrArray.Append(curveArray);
                    }
                }
            }
            return curveArrArray;
        }

        public void GetCurveArrArray()
        {
            //选择图元
            //  pickedElements = new List<Element>();
            List<Solid> solidsList = new List<Solid>();
            List<Reference> refsList = new List<Reference>();
            try
            {
                refsList = uidoc.Selection.PickObjects(ObjectType.Element, new FilteredColumnAndBeams(), "选择生成区域的梁和柱").ToList();
            }
            catch (Exception)
            {

                result = false;
            }
            if (refsList.Count == 0)
            {
                result = false;
            }
            else
            {
                foreach (var refs in refsList)
                {
                    GeometryElement geometryElement = doc.GetElement(refs).get_Geometry(new Options());
                    foreach (var geoObject in geometryElement)
                    {
                        Solid solid = geoObject as Solid;
                        if (solid != null)
                        {
                            if (solid.Volume != 0)
                            {
                                solidsList.Add(solid);
                            }
                        }
                    }
                }
                if (solidsList.Count < 1)
                {
                    result = false;
                }
                else
                {
                    Solid mixSolid = null;
                    mixSolid = BooleanOperationsUtils.ExecuteBooleanOperation(solidsList[0], solidsList[1], BooleanOperationsType.Union);
                    for (int i = 1; i < solidsList.Count; i++)
                    {
                        mixSolid = BooleanOperationsUtils.ExecuteBooleanOperation(mixSolid, solidsList[1], BooleanOperationsType.Union);
                    }
                    if (mixSolid == null)
                    {
                        result = false;
                    }
                    else
                    {
                        double max = 0;
                        double elevation = 0;
                        foreach (var elem in structuralBeams)
                        {
                            elevation = elem.get_Parameter(BuiltInParameter.STRUCTURAL_ELEVATION_AT_BOTTOM).AsDouble();
                            if (max < elevation)
                            {
                                max = elevation;
                            }
                        }
                        XYZ normal = XYZ.BasisZ;
                        XYZ origin = new XYZ(0, 0, max - 0.1);
                        Plane plane = Plane.CreateByNormalAndOrigin(normal, origin);
                        List<CurveLoop> curveLoops = new List<CurveLoop>();
                        Solid newSolid = BooleanOperationsUtils.CutWithHalfSpace(mixSolid, plane);
                        CurveArray curveArray = null;
                        if (newSolid != null)
                        {
                            FaceArrayIterator iterator = newSolid.Faces.ForwardIterator();
                            while (iterator.MoveNext())
                            {
                                PlanarFace planarFace = iterator.Current as PlanarFace;
                                if (planarFace != null)
                                {
                                    if (planarFace.FaceNormal.IsAlmostEqualTo(new XYZ(0, 0, 1)))
                                    {
                                        //删除最大的闭合线
                                        curveLoops = planarFace.GetEdgesAsCurveLoops().OrderBy(x => x.GetExactLength()).ToList();
                                        int index = curveLoops.IndexOf(curveLoops.Last());
                                        curveLoops.RemoveAt(index);
                                        break;
                                    }
                                }
                            }
                            if (curveLoops.Count != 0)
                            {
                                foreach (CurveLoop curveLoop in curveLoops)
                                {
                                    CurveLoopIterator loopIterator = curveLoop.GetCurveLoopIterator();
                                    curveArray = new CurveArray();
                                    while (loopIterator.MoveNext())
                                    {
                                        curveArray.Append(loopIterator.Current);
                                    }
                                    curveArrArray.Append(curveArray);
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }
        }

    }
}
