using Autodesk.Revit.DB;
using System.Linq;


namespace CodeInTangsengjiewa.BinLibrary.Extensions
{
  public  static  class LineExtension
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

        public  static  XYZ

    }
}
