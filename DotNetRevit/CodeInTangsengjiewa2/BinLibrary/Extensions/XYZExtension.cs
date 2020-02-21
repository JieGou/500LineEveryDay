using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Transform = Autodesk.Revit.DB.Transform;

namespace CodeInTangsengjiewa2.BinLibrary.Extensions
{
    public static class XYZExtension
    {
        public static XYZ xyComponent(this XYZ po)
        {
            return new XYZ(po.X, po.Y, 0);
        }

        public static XYZ getRandomNorm(this XYZ vec)
        {
            XYZ norm = new XYZ(-vec.X + vec.Z, vec.X + vec.Z, -vec.Y - vec.X);
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

            var poIntrans = transform.Inverse.OfPoint(po);

            var po_proj = new XYZ(poIntrans.X, poIntrans.Y, 0);

            var po_projInWorld = transform.OfPoint(po_proj);

            return po_projInWorld;
        }

        public static XYZ PorjectTo(this XYZ po, Line l)
        {
            var dir = l.Direction;
            var transform = Transform.Identity; //恒等变换.感觉有点像设置默认值.

            transform.Origin = l.Origin;
            transform.BasisX = dir.RandVerticalVec().Normalize();
            transform.BasisY = (l.Direction.CrossProduct(transform.BasisX)).Normalize();

            var poInTrans = transform.Inverse.OfPoint(po);
            var poInTransProj = new XYZ(0, 0, poInTrans.Z);
            var poInTransPorjInWorld = transform.OfPoint(poInTransProj);

            return poInTransPorjInWorld;
        }

        /// <summary>
        /// 获取任意垂直向量
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static XYZ RandVerticalVec(this XYZ vec)
        {
            XYZ ret = new XYZ(-vec.Y + vec.Z, vec.X + vec.Z, -vec.Y - vec.X);
            return ret;
        }
    }
}