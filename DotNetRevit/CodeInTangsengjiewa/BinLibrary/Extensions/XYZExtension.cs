using System.Threading;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa.BinLibrary.Extensions
{
    public static class XYZExtension
    {
        public static XYZ xyzComponent(this XYZ po)
        {
            return new XYZ(po.X, po.Y, po.Z);
        }

        public static XYZ getRandomNorm(this XYZ vec)
        {
            XYZ norm = new XYZ(-vec.Y + vec.Z, vec.X + vec.Z, -vec.Y - vec.X);
            return norm.Normalize();
        }

        /// <summary>
        /// 将点投影到面
        /// </summary>
        /// <param name="po"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static XYZ ProjectTo(this XYZ po, Plane p)
        {
            var transform = Transform.Identity;
            transform.Origin = p.Origin;
            transform.BasisX = p.XVec;
            transform.BasisY = p.YVec;
            transform.BasisZ = p.Normal;

            var poInTrans = transform.Inverse.OfPoint(po);

            var po_proj = new XYZ(poInTrans.X, poInTrans.Y, 0);

            var po_projInWorld = transform.OfPoint(po_proj);

            return po_projInWorld;
        }


        public static XYZ ProjctTo(this XYZ po, Line l)
        {
            var dir = l.Direction;
            var transform = Transform.Identity;

            transform.Origin = l.Origin;
            transform.BasisX = dir.RandVerticalVec().Normalize();
            transform.BasisY = (l.Direction.CrossProduct(transform.BasisX)).Normalize();
            transform.BasisZ = l.Direction;

            var poInTrans = transform.Inverse.OfPoint(po);
            var poInTransProj = new XYZ(0, 0, poInTrans.Z);

            var poInTransProjInWorld = transform.OfPoint(poInTransProj);

            return poInTransProjInWorld;
        }


        public static XYZ RandVerticalVec(this XYZ vec)
        {
            XYZ ret = new XYZ(-vec.Y + vec.Z, vec.X + vec.Z, -vec.Y - vec.X);
            return ret;
        }

        // public static double AngleTo2(this XYZ vec1,XYZ vec2)
        // {
        //     double angle = vec1.DotProduct(vec2) / (vec1.GetLength()+vec2.GetLength());
        //
        //
        //     return 
        // } 
    }
}