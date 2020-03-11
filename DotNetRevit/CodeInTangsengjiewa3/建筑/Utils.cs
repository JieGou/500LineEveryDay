using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa3.建筑
{
    class Utils
    {
        public static bool CutBeam(FamilyInstance beam, XYZ cutPoint)
        {
            var result = false;
            if (beam.Category.Id.IntegerValue != (int) BuiltInCategory.OST_StructuralFraming)
            {
                throw new Exception("element being cut is not beam");
            }
            var locationline = beam.Location as LocationCurve;
            ///
            ///
            ///未写完
            /// 
            return result;
        }
    }
}