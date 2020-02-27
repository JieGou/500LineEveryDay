using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa2.BinLibrary.Helpers
{
    public static class CurveHelper
    {
        /// <summary>
        /// 根据一点和一条曲线,求得点到曲线连线想垂直的切点.
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="po"></param>
        /// <returns></returns>
        public static List<XYZ> GetPrependicularPointList(Curve curve, XYZ po)
        {
            var result = default(List<XYZ>);
            // if (curve is Arc arc)
            // {
            //     arc.ComputeDerivatives()
            // }
            return result;
        }
    }
}