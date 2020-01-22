using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.DB.Events;

namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.BinLibrary.Utils
{
    class Utils
    {
        /// <summary>
        /// gets solid objects of specified element.
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static List<Solid> GetElementSolids(Element elem, Options opt = null)
        {
            if (null == elem)
            {
                return null;
            }

            if (null == opt)
            {
                opt = new Options();
            }

            List<Solid> solids = new List<Solid>();
            GeometryElement gElem = null;

            try
            {
                gElem = elem.get_Geometry(opt);
                IEnumerator<GeometryObject> gIter = gElem.GetEnumerator();
                gIter.Reset();

                while (gIter.MoveNext())
                {
                    solids.AddRange(GetSolids(gIter.Current));
                }
            }
            catch (Exception ex)
            {
                //In Revit, sometime get the geometry will failed. 
                string error = ex.Message;
            }

            return solids;
        }


        /// <summary>
        /// gets all solid object from geometry object
        /// </summary>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static List<Solid> GetSolids(GeometryObject gObj)
        {
            List<Solid> solids = new List<Solid>();

            if (gObj is Solid)
            {
                Solid solid = gObj as Solid;

                if (solid.Faces.Size > 0 && solid.Volume > 0) // some solid may have not any face

                    solids.Add(gObj as Solid);
            }
            else if (gObj is GeometryInstance) //find solids from geometry
            {
                IEnumerator<GeometryObject> gIter2 =
                    (gObj as GeometryInstance).GetInstanceGeometry().GetEnumerator();
                gIter2.Reset();

                while (gIter2.MoveNext())
                {
                    solids.AddRange(GetSolids(gIter2.Current));
                }
            }
            else if (gObj is GeometryElement) //find solids from GeometryElement,this will not happen at all
            {
                IEnumerator<GeometryObject> gIter2 = (gObj as GeometryElement).GetEnumerator();
                gIter2.Reset();

                while (gIter2.MoveNext())
                {
                    solids.AddRange(GetSolids(gIter2.Current));
                }
            }

            return solids;
        }

        /// <summary>
        /// draws model curves by given geometry curves
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="curves"></param>
        /// <param name="reviseTrf"></param>
        /// <returns></returns>
        public static List<ElementId> DrawModelCurves(Document doc, List<Curve> curves, Transform reviseTrf = null)
        {
            List<ElementId> newIds = new List<ElementId>();

            foreach (Curve crv in curves)
            {
                if (crv.IsBound)
                {
                    Curve reviseCrv =
                        (null != reviseTrf && !reviseTrf.IsIdentity)
                            ? crv.CreateTransformed(reviseTrf)
                            : crv;
                    newIds.Add(creatModelCurve(doc, reviseCrv));
                }
            }

            return newIds;
        }

        public static ElementId creatModelCurve(Document document, Curve curve, SketchPlane sp = null)
        {
            Line line = curve as Line;
            Arc arc = curve as Arc;
            Ellipse ellipse = curve as Ellipse;
            HermiteSpline spline = curve as HermiteSpline;
            NurbSpline nbSpline = curve as NurbSpline;

            if (line != null && null == sp)
            {
                XYZ normal = getVertVec(line.Direction).Normalize();
                XYZ origin = line.GetEndPoint(0);
                sp = SketchPlane.Create(document, Plane.CreateByNormalAndOrigin(normal, origin));
            }

            else if (arc != null && null == sp)
            {
                XYZ normal = arc.Normal;
                sp = SketchPlane.Create(document, Plane.CreateByNormalAndOrigin(normal, arc.Center));
            }

            else if (ellipse != null && null == sp)
            {
                XYZ normal = ellipse.Normal;
                sp = SketchPlane.Create(document, Plane.CreateByNormalAndOrigin(normal, ellipse.Center));
            }

            else if (spline != null && null == sp)
            {
                Transform tran = spline.ComputeDerivatives(0, false);
                XYZ normal = getVertVec(tran.BasisX).Normalize();
                XYZ origin = spline.GetEndPoint(0);
                sp = SketchPlane.Create(document, Plane.CreateByNormalAndOrigin(normal, origin));
            }

            else if (nbSpline != null && null == sp)
            {
                Transform tran = nbSpline.ComputeDerivatives(0, false);
                XYZ normal = getVertVec(tran.BasisX).Normalize();
                XYZ origin = nbSpline.GetEndPoint(0);
                sp = SketchPlane.Create(document, Plane.CreateByNormalAndOrigin(normal, origin));
            }

            if (sp == null)
            {
                throw new ArgumentException("Not valid sketchplan to creat curve:" + curve.GetType().Name);
            }

            //create model line whth curve and specified sketch plane.
            ModelCurve mCurve = document.Create.NewModelCurve(curve, sp);
            return (null != mCurve) ? mCurve.Id : ElementId.InvalidElementId;
        }

        /// <summary>
        /// gets one vertical vector of given vector, the return vector is not normalized.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        private static XYZ getVertVec(XYZ vec)
        {
            XYZ ret = new XYZ(-vec.Y + vec.Z, vec.X + vec.Z, -vec.Y - vec.X);
            return ret;
        }

        public static bool CanMakeBound(XYZ end0, XYZ end1)
        {
            try
            {
                Line ln = Line.CreateBound(end0, end1);
                return (null != ln);
            }

            catch (System.Exception)
            {
                return false;
            }
        }
    }
}