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
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_CalculateAreaOfShipment : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            var wall =
                sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Wall)).GetElement(doc) as Wall;

            var facesoutRef = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Exterior);
            //HostObjectUtils: Revie.DB命名空间下的类,里面还有获取顶面和底面的方法.
            //ShellLayerType: Revie.DB命名空间下的枚举,Interior = 0,Exterior = 1;
            var facesinRef = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Interior);

            var faceout = wall.GetGeometryObjectFromReference(facesoutRef.First()) as Face;
            var facein = wall.GetGeometryObjectFromReference(facesinRef.First()) as Face;

            var area = default(double);
            area += faceout.Area;
            area += facein.Area;
            area = UnitUtils.ConvertFromInternalUnits(area, DisplayUnitType.DUT_SQUARE_METERS);
            //UnitUtils: Revie.DB命名空间下的静态类,提供了转换单位的静态方法.

            area = Math.Round(area, 3);
            MessageBox.Show(area.ToString() + " m^2");

            return Result.Succeeded;
        }
    }
}