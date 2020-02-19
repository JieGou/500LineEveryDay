using Autodesk.Revit.DB;
using System.Linq;


namespace CodeInTangsengjiewa.BinLibrary.Extensions
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

            var pointOnline = lineorigin + linedir;

            var trans = Transform.Identity;
            trans.Origin = p.Origin;
            trans.BasisX = p.XVec;
            trans.BasisZ = p.Normal;

            var point1 = lineorigin;
            var point2 = pointOnline;

            var point1Intrans = trans.Inverse.OfPoint(point1);
            var point2Intrans = trans.Inverse.OfPoint(point2);

            point1Intrans = new XYZ(point1Intrans.X, point1Intrans.Y, 0);
            point2Intrans = new XYZ(point2Intrans.X, point2Intrans.Y, 0);

            var point1Inworld = trans.OfPoint(point1Intrans);
            var point2Inworld = trans.OfPoint(point2Intrans);

            var newLineInPlan = Line.CreateBound(point1Inworld, point2Inworld);

            var unboundnewLine = newLineInPlan.Clone() as Line;
            unboundnewLine.MakeUnbound();

            var unboundOriginalLine = line.Clone() as Line;
            unboundOriginalLine.MakeUnbound();

            return unboundnewLine.Intersect_cus(unboundOriginalLine);
        }

        public static XYZ Intersect_cus(this Line line1, Line line2)
        {
            var compareResunlt = line1.Intersect(line2, out IntersectionResultArray intersectResult);

            if (compareResunlt != SetComparisonResult.Disjoint)
            {
                var result = intersectResult.get_Item(0).XYZPoint;
                return result;
            }

            return null;
        }
    }
}