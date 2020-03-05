using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CodeInTangsengjiewa3.BinLibrary.Extensions
{
    public static class PointExtension
    {
        /// <summary>
        /// 将指定点投影到曲线上.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static XYZ ProjectToXLine(this XYZ point, Line line)
        {
            Line l1 = line.Clone() as Line;
            if (l1.IsBound)
            {
                l1.MakeUnbound();
            }

            return l1.Project(point).XYZPoint;
        }

        private static double precision = 1e-6;

        public static bool IsEqual(double d1, double d2)
        {
            double diff = Math.Abs(d1 - d2);
            return diff < precision;
        }

        /// <summary>
        /// 判断点是否在直线上
        /// </summary>
        /// <param name="point"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsOnLine(this XYZ point, Line line)
        {
            XYZ end1 = line.GetEndPoint(0);
            XYZ end2 = line.GetEndPoint(1);

            XYZ vec_pToEnd1 = end1 - point;
            XYZ vec_PToEnd2 = end2 - point;

            if (point.DistanceTo(end1) < precision || point.DistanceTo(end2) < precision)
            {
                return true;
            }
            if (vec_pToEnd1.IsOppositeDirection(vec_PToEnd2))
            {
                return true;
            }

            return false;
        }

        public static bool IsOnXLine(this XYZ po, Line l)
        {
            double precision = 1e-6;
            var l1 = l.Clone() as Line;
            l1.MakeUnbound();
            if (po.DistanceTo(l1) < precision)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 求点到直线的距离.系统自带的Line.Distance(point)遇到line是有界的时候,会输出point到Line的端点的长度的情况.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="xLine"></param>
        /// <returns></returns>
        public static double DistanceTo(this XYZ p1, Line xLine)
        {
            double result = double.NegativeInfinity;
            XYZ p1_onLine = p1.ProjectToXLine(xLine);
            result = p1.DistanceTo(p1_onLine);
            return result;
        }
    }
}