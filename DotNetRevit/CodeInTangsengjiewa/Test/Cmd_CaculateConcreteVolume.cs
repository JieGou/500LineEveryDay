using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Windows;
using CodeInTangsengjiewa.BinLibrary.Extensions;


namespace CodeInTangsengjiewa.Test
{
    /// <summary>
    /// 计算元素体积
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_CaculateConcreteVolume : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            var ele = sel.PickObject(ObjectType.Element).GetElement(doc);

            var options = new Options();
            options.DetailLevel = ViewDetailLevel.Fine;

            var geometryelement = ele.get_Geometry(options);

            var volume = getVolumes(geometryelement);
            var volumesstring = Math.Round(volume, 3).ToString();

            MessageBox.Show(volumesstring + " m^3");

            return Result.Succeeded;
        }

        public double getVolumes(GeometryElement geoEle)
        {
            double result = default(double); //得到该类型的默认值.

            var geoenu = geoEle.GetEnumerator();

            while (geoenu.MoveNext())
            {
                var currentgeo = geoenu.Current;

                if (currentgeo is Solid solid)
                {
                    result += solid.Volume;
                }
                else if (currentgeo is GeometryInstance geoins)
                {
                    var temgeoele = geoins.SymbolGeometry;
                    var geoenu1 = temgeoele.GetEnumerator();

                    while (geoenu1.MoveNext())
                    {
                        var currentgeo1 = geoenu1.Current;

                        if (currentgeo1 is Solid solid1)
                        {
                            result += solid1.Volume;
                        }
                    }
                }
            }

            result = UnitUtils.ConvertFromInternalUnits(result, DisplayUnitType.DUT_CUBIC_METERS);
            return result;
        }
    }
}