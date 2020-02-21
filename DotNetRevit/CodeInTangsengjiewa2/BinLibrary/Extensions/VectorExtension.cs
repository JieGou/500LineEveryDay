using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa2.BinLibrary.Extensions
{
    public static class VectorExtension
    {
        private static double precision = 1e-6;

        public static bool IsParallel(this XYZ vector1, XYZ vector2)
        {
            return vector1.IsSameDirection(vector2) || vector1.IsOppositeDirection(vector2);
        }

        public static bool IsSameDirection(this XYZ dir1, XYZ dir2)
        {
            bool result = false;
            double dotproduct = dir1.Normalize().DotProduct(dir2.Normalize());

            if (Math.Abs(dotproduct - 1) < precision)
            {
                result = true;
            }

            return result;
        }

        public static bool IsOppositeDirection(this XYZ dir1, XYZ dir2)
        {
            bool result = false;
            double dotproduct = dir1.Normalize().DotProduct(dir2.Normalize());

            if (Math.Abs(dotproduct + 1) < precision)
            {
                result = true;
            }

            return result;
        }
    }
}