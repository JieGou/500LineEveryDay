using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using CodeInTangsengjiewa.BinLibrary.Helpers;
using CodeInTangsengjiewa.BinLibrary.RevitHelper;
using CodeInTangsengjiewa.Test.UIs;

namespace CodeInTangsengjiewa.Test
{
    /// <summary>
    /// 在轴线交点处生成柱子
    /// </summary>
    class Cmd_CreateColumnAccordingGridIntersection : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = doc.ActiveView;

            //filter target columntypes




            return Result.Succeeded;
        }
    }
}