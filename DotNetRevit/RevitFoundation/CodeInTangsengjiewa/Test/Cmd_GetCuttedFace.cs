using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa.BinLibrary.Extensions;
using ReferenceExtension = CodeInTangsengjiewa.BinLibrary.Extensions.ReferenceExtension;
using Transaction = System.Transactions.Transaction;

/// <summary>
/// 未测试通过
/// </summary>
namespace CodeInTangsengjiewa.Test
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class Cmd_GetCuttedFace : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            var beam = ReferenceExtension.GetElement(sel.PickObject(ObjectType.Element), doc) as FamilyInstance;

            var getcuttingelements = JoinGeometryUtils.GetJoinedElements(doc, beam).ToList();

            var beamsolid =
                GeometryObjectExtension.GetSolidOfGeometryObject(beam.get_Geometry(new Options()
                {
                    DetailLevel = ViewDetailLevel.Fine
                }));

            var othersolids =
                getcuttingelements.Select(m => m.GetElement(doc)
                                              .get_Geometry(new Options() {DetailLevel = ViewDetailLevel.Fine}));

            // var resultsolid = default(Solid);

            foreach (var othersolid in othersolids)
            {
                //resultsolid =  BooleanOperationsUtils.ExecuteBooleanOperation(beamsolid as Solid, othersolids.First(),BooleanOperationsType.Difference)
            }

            return Result.Succeeded;
        }
    }
}
