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
using CodeInTangsengjiewa3.BinLibrary.Extensions;

namespace CodeInTangsengjiewa3.Test
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_CalculateConcreteVolume : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            var ele = sel.PickObject(ObjectType.Element).GetElement(doc);
            var options = new Options();
            options.DetailLevel = ViewDetailLevel.Fine;

            var geometryElement = ele.get_Geometry(options);
            var volume = GetVolume(geometryElement);
            MessageBox.Show(Math.Round(volume, 3).ToString());
            return Result.Succeeded;
        }

        public double GetVolume(GeometryElement geoEle)
        {
            double result = default(double);
            var geoEnu = geoEle.GetEnumerator();

            while (geoEnu.MoveNext())
            {
                var currentGeo = geoEnu.Current;
                if (currentGeo is Solid solid)
                {
                    result += solid.Volume;
                }
                else if (currentGeo is GeometryInstance geoInstance)
                {
                    var temGeoele = geoInstance.SymbolGeometry;
                    var geoEnum2 = temGeoele.GetEnumerator();
                    while (geoEnum2.MoveNext())
                    {
                        var currentGeo2 = geoEnum2.Current;
                        if (currentGeo2 is Solid solid2)
                        {
                            result += solid2.Volume;
                        }
                    }
                }
            }
            result = UnitUtils.ConvertFromInternalUnits(result, DisplayUnitType.DUT_CUBIC_METERS);
            return result;
        }
    }
}