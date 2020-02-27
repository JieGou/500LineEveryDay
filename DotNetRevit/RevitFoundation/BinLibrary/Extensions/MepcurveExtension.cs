using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa2.BinLibrary.Extensions
{
    public static class MepcurveExtension
    {
        public static Line LocationLine(this MEPCurve mep)
        {
            Line result = null;
            result = (mep.Location as LocationCurve).Curve as Line;
            return result;
        }
    }
}