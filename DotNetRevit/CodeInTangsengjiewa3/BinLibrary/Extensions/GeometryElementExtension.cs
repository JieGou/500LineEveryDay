using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa3.BinLibrary.Extensions
{
    public static class GeometryElementExtension
    {
        public static List<GeometryObject> GetGemetries(this GeometryElement geoele)
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
    }
}