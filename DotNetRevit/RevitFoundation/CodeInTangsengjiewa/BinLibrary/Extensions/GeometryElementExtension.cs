using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa.BinLibrary.Extensions
{
    public static class GeometryElementExtension
    {
        public static List<GeometryObject> GetGeometries(this GeometryElement geoele)
        {
            List<GeometryObject> result = new List<GeometryObject>();
            var enu = geoele.GetEnumerator();

            while (enu.MoveNext())
            {
                var geoobj = enu.Current as GeometryObject;

                if (geoobj != null)
                {
                    result.Add(geoobj);
                }
            }

            return result;
        }

        public static List<Face> GetFaces(this GeometryElement geoele)
        {
            List<Face> result = new List<Face>();
            var geoobjs = geoele.GetGeometries();

            foreach (GeometryObject geoobj in geoobjs)
            {
                result.AddRange(geoobj.GetFacesOfGeometryObject());
            }

            return result;
        }

        public static List<Edge> GetEdges(this GeometryElement geoele)
        {
            List<Edge> result = new List<Edge>();
            var geoobjs = geoele.GetGeometries();

            foreach (GeometryObject geoobj in geoobjs)
            {
                result.AddRange(geoobj.GetEdgesOfGeometryObject());
            }

            return result;
        }

        public static List<XYZ> GetPoints(this GeometryElement geoele)
        {
            List<XYZ> result = new List<XYZ>();
            var geoedges = geoele.GetEdges();
            var points = new List<XYZ>();

            foreach (Edge edge in geoedges)
            {
                var curve = edge.AsCurve();
                var startpoint = curve.GetEndPoint(0);
                var endpoint = curve.GetEndPoint(1);

                //判断点是都位置上重合,如果不重合,则添加进结果别表
                var startflag = false;
                var endflag = false;
                points.ForEach(m =>
                {
                    if (m.DistanceTo(endpoint) < 1e-6) startflag = true;

                    if (m.DistanceTo(endpoint) < 1e-6) endflag = true;
                });

                if (!startflag) points.Add(startpoint);

                if (!endflag) points.Add(endpoint);
            }

            result = points;
            return result;
        }
    }
}