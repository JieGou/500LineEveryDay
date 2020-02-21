using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa2.BinLibrary.Extensions
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
            var lineorigin = line.Origin;
            var linedir = line.Direction;

            var pointOnLine = lineorigin + linedir;

            var trans = Transform.Identity;
            trans.Origin = p.Origin;
            trans.BasisX = p.XVec;
            trans.BasisY = p.YVec;
            trans.BasisZ = p.Normal;

            var point1 = lineorigin;
            var point2 = pointOnLine;

            var point1Intrans = trans.Inverse.OfPoint(point1);
            var point2Intrans = trans.Inverse.OfPoint(point2);

            var point1Inworld = trans.OfPoint(point1Intrans);
            var point2Inworld = trans.OfPoint(point2Intrans);

            var newlineInPlan = Line.CreateBound(point1Inworld, point2Inworld);

            var unboundnewline = newlineInPlan.Clone() as Line;
            unboundnewline.MakeUnbound(); //修改曲线不影响原曲线的主体.

            var unboundOriginalLine = line.Clone() as Line;
            unboundOriginalLine.MakeUnbound();

            return unboundnewline.Intersect_cus(unboundOriginalLine);
        }

        public static XYZ Intersect_cus(this Line line1, Line line2)
        {
            var compareResult = line1.Intersect(line2, out IntersectionResultArray intersectResult);
            //有两个返回值:compareResult:Enum; intersectResult:IntersectionResultArray;
            //line1.Intersect: 求曲线与指定曲线的交点,并返回结果
            if (compareResult != SetComparisonResult.Disjoint)
            {
                var result = intersectResult.get_Item(0).XYZPoint;
                return result;
            }

            return null;
        }
    }
}