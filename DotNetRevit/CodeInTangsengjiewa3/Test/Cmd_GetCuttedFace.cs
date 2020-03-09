using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa3.BinLibrary.Extensions;

namespace CodeInTangsengjiewa3.Test
{
    /// <summary>
    /// 不能正常工作!!!!!!!!!!!!!!!!
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_GetCuttedFace : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            var beam = sel.PickObject(ObjectType.Element).GetElement(doc) as FamilyInstance;
            var getcuttingelments = JoinGeometryUtils.GetJoinedElements(doc, beam).ToList();

            // beam.get_Geometry()
            //     beam.GetGeometryObjectFromReference()

            var beamSolid = beam.get_Geometry(new Options() {DetailLevel = ViewDetailLevel.Fine})
                .GetSolidOfGeometryObject();

            //有问题
            var otherSolids =
                getcuttingelments.Select(m => m.GetElement(doc)
                                             .get_Geometry(new Options() {DetailLevel = ViewDetailLevel.Fine})
                                             .GetSolidOfGeometryObject());
            var resultSolid = default(Solid);
            foreach (var othrerSolid in otherSolids)
            {
                resultSolid =
                    BooleanOperationsUtils.ExecuteBooleanOperation(beamSolid as Solid, otherSolids.First().First(),
                                                                   BooleanOperationsType.Difference);
            }

            return Result.Succeeded;
        }
    }
}