using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa3.BinLibrary.Extensions
{
    public static class GeometryObjectExtension
    {
        public static IList<Face> GetFacesOfGeometryObject(this GeometryObject geoobj)
        {
            IList<Face> result = new List<Face>();
            List<Face> temresult = new List<Face>();

            if (geoobj is GeometryElement)
            {
                GeometryElement geoele = geoobj as GeometryElement;

                foreach (GeometryObject geoitem in geoele)
                {
                    temresult.AddRange(GetFacesOfGeometryObject(geoitem));
                }
            }
            else if (geoobj is GeometryInstance)
            {
                GeometryElement geoele = (geoobj as GeometryInstance).SymbolGeometry;

                foreach (GeometryObject obj in geoele)
                {
                    temresult.AddRange(GetFacesOfGeometryObject(obj));
                }
            }
            else if (geoobj is Solid)
            {
                Solid solid = geoobj as Solid;

                foreach (Face face in solid.Faces)
                {
                    temresult.Add(face);
                }
            }

            result = temresult;
            return result;
        }

        public static IList<Solid> GetSolidOfGeometryObject(this GeometryObject geoobj)
        {
            IList<Solid> result = new List<Solid>();
            List<Solid> temresult = new List<Solid>();

            if (geoobj is GeometryElement)
            {
                GeometryElement geoele = geoobj as GeometryElement;

                foreach (GeometryObject geoitem in geoele)
                {
                    temresult.AddRange(GetSolidOfGeometryObject(geoitem));
                }
            }

            else if (geoobj is GeometryInstance)
            {
                GeometryElement geoele = (geoobj as GeometryInstance).SymbolGeometry;

                foreach (GeometryObject obj in geoele)
                {
                    if (obj is Solid)
                    {
                        temresult.AddRange(GetSolidOfGeometryObject(obj));
                    }
                }
            }

            else if (geoobj is Solid)
            {
                Solid solid = geoobj as Solid;
                temresult.Add(solid);
            }

            result = temresult;
            return result;
        }

        public static IList<Edge> GetEdgesOfGeometryObject(this GeometryObject geoobj)
        {
            IList<Edge> result = new List<Edge>();
            List<Edge> temresult = new List<Edge>();

            if (geoobj is GeometryElement)
            {
                GeometryElement geoele = geoobj as GeometryElement;

                foreach (GeometryObject geoitem in geoele)
                {
                    temresult.AddRange(GetEdgesOfGeometryObject(geoitem));
                }
            }

            else if (geoobj is GeometryInstance)
            {
                GeometryElement geoele = (geoobj as GeometryInstance).SymbolGeometry;

                foreach (GeometryObject obj in geoele)
                {
                    if (obj is Solid)
                    {
                        temresult.AddRange(GetEdgesOfGeometryObject(obj));
                    }
                }
            }

            else if (geoobj is Solid)
            {
                Solid solid = geoobj as Solid;

                foreach (Face face in solid.Faces)
                {
                    temresult.AddRange(GetEdgesOfGeometryObject(face));
                }
            }

            else if (geoobj is Face)
            {
                Face face = geoobj as Face;

                foreach (EdgeArray edgeArray in face.EdgeLoops)
                {
                    var enu = edgeArray.GetEnumerator();

                    while (enu.MoveNext())
                    {
                        var edge = enu.Current as Edge;

                        if (edge != null)
                        {
                            temresult.Add(edge);
                        }
                    }
                }
            }

            else if (geoobj is Edge)
            {
                temresult.Add(geoobj as Edge);
            }

            result = temresult;
            return result;
        }
    }
}