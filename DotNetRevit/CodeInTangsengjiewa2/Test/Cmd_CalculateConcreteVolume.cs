using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace CodeInTangsengjiewa2.Test
{
    /// <summary>
    /// 计算元素的体积: 不能区分材质
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CalculateConcreteVolume : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            var ele = sel.PickObject(ObjectType.Element).GetElement(doc);

            var options = new Options();
            options.DetailLevel = ViewDetailLevel.Fine;

            var geometyrelement = ele.get_Geometry(options);
            // ele.get_Geometry, 在定义里能到,在api.chm里却看不到.

            var volumn = GetVolumns(geometyrelement);
            var volumnstring = Math.Round(volumn, 3).ToString();

            MessageBox.Show(volumnstring + " m^3");

            return Result.Succeeded;
        }

        public double GetVolumns(GeometryElement geoEle)
        {
            double result = default(double);

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

            //单位转换:立方英尺转立方米
            result = UnitUtils.ConvertFromInternalUnits(result, DisplayUnitType.DUT_CUBIC_METERS);
            return result;
        }
    }
}