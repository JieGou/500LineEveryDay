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
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_SurfaceTest : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = uidoc.ActiveView;

            var floor =
                sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Floor)).GetElement(doc) as Floor;

            var faces = HostObjectUtils.GetTopFaces(floor);
            //HostObjectUtils: revit.db命名空间下的静态类, 只有3个静态方法,返回值是引用的集合:IList<Reference> GetBottomFaces(HostObject hostObject);
            MessageBox.Show(faces.Count.ToString());

            return Result.Succeeded;
        }
    }
}