using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa2.建筑
{
    class Utils
    {
        public static bool CubBeam(FamilyInstance beam, XYZ cutpoint)
        {
            var result = false;
            if (beam.Category.Id.IntegerValue != (int) BuiltInCategory.OST_StructuralFraming)
            {
                throw new Exception("Element being cut is not Beam!");
            }
            var locationline = beam.Location as LocationCurve;

            return result;
        }
    }
}