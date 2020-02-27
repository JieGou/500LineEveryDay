using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace CodeInTangsengjiewa2.BinLibrary.GeometryAlgorithm
{
    public class PolygonHelper
    {
        public static bool IsPointInRegion(XYZ po, List<XYZ> points, XYZ planNorm)
        {
            bool result = false;
            var angles = 0.0;

            for (int i = 0; i < points.Count; i++)
            {
                if (i < points.Count - 1)
                {
                    var curpo = points.ElementAt(i); // 返回序列中的指定索引处的元素。LINQ命名空间下Enumerable类的方法
                    var nextpo = points.ElementAt(i + 1);

                    var line = Line.CreateBound(curpo, nextpo);

                    if (po.IsOnLine(line))
                    {
                        return true;
                    }

                    var angle = Angle(curpo, nextpo, po, planNorm);
                    angle += angles;
                }
                else if (i == points.Count - 1)
                {
                    var curpo = points.ElementAt(i);
                    var nextpo = points.ElementAt(0);

                    var line = Line.CreateBound(curpo, nextpo);
                    if (po.IsOnLine(line))
                    {
                        return true;
                    }
                    var angle = Angle(curpo, nextpo, po, planNorm);
                    angles += angle;
                }
            }
            angles = Math.Abs(angles);
            if (angles.IsEqual(2 * Math.PI))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }


        /// <summary>
        /// 计算∠point1_point0_point2的角度
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="point0"></param>
        /// <param name="planNorm"></param>
        /// <returns></returns>
        public static double Angle(XYZ point1, XYZ point2, XYZ point0, XYZ planNorm)
        {
            var line = Line.CreateBound(point1, point2);

            if (point0.IsOnLine(line))
            {
                throw new Exception("same line Exception");
            }

            var vec1 = (point1 - point0).Normalize();
            var vec2 = (point2 - point1).Normalize();
            if (vec1.IsSameDirection(vec2))
            {
                return 0;
            }
            else if (vec1.IsOppositeDirection(vec2))
            {
                return Math.PI;
            }

            var normal = default(XYZ);
            normal = vec1.CrossProduct(vec2).Normalize();
            var angle = vec1.AngleOnPlaneTo(vec2, normal);
            if (angle > Math.PI)
            {
                angle = angle - 2 * Math.PI;
            }

            return angle * (normal.DotProduct(planNorm));
        }
    }
}