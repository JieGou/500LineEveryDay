using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa3.BinLibrary.Extensions
{
    public static class ColorExtension
    {
        public static Color InverseColor(this Color color)
        {
            var newcolor = default(Color);
            var newR = (byte) (255 - color.Red);
            var newG = (byte) (255 - color.Green);
            var newB = (byte) (255 - color.Blue);

            newcolor = new Color(newR, newG, newB);

            return newcolor;
        }

        public static Color ToRvtColor(this System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B);
        }
    }
}