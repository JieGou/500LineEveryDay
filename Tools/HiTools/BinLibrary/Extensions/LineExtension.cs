using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa3.BinLibrary.Extensions
{
    public static class LineExtension
    {
        public static XYZ StartPoint(this Line line)
        {
            if (line.IsBound)
            {
                return line.GetEndPoint(0);
            }
            return null;
        }

        public static XYZ EndPoint(this Line line)
        {
            if (line.IsBound)
            {
                return line.GetEndPoint(1);
            }
            return null;
        }

        public static XYZ Intersect_cus(this Line line, Plane p)
        {
            var lineOrigin = line.Origin;
            var lineDirection = line.Direction;

            var pointOnline = lineOrigin + lineDirection;

            var trans = Transform.Identity;
            trans.Origin = p.Origin;
            trans.BasisX = p.XVec;
            trans.BasisY = p.YVec;
            trans.BasisZ = p.Normal;

            var point1 = lineOrigin;
            var point2 = pointOnline;

            var point1Intrans = trans.Inverse.OfPoint(point1);
            var point2Intrans = trans.Inverse.OfPoint(point2);

            var point1InWorld = trans.OfPoint(point1Intrans);
            var point2InWorld = trans.OfPoint(point2Intrans);

            var newlineInPlan = Line.CreateBound(point1InWorld, point2InWorld);

            var unboundNewLine = newlineInPlan.Clone() as Line;
            unboundNewLine.MakeUnbound(); //修改曲线不影响原曲线的主体

            var unboundOriginalLine = line.Clone() as Line;
            unboundOriginalLine.MakeUnbound();

            return unboundNewLine.Intersect_cus(unboundOriginalLine);
        }

        public static XYZ Intersect_cus(this Line line1, Line line2)
        {
            var compareResult = line1.Intersect(line2, out IntersectionResultArray intersectionResult);
            //有两个返回值: compareResult:Enum; intersecResult:IntersectionResultArray;
            //line1.Intersect: 求曲线与特定曲线的交点,并返回结果
            if (compareResult != SetComparisonResult.Disjoint)
            {
                var result = intersectionResult.get_Item(0).XYZPoint;
                return result;
            }
            return null;
        }
    }
}