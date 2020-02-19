using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using CodeInTangsengjiewa.BinLibrary.Extensions;
using UIFrameworkServices;

namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.注释
{
    [Transaction(TransactionMode.Manual)]
    class Cmd_FlipDimension : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            var dim = sel
                .PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Dimension))
                .GetElement(doc) as Dimension;
            sel.SetElementIds(new List<ElementId>() {dim.Id});

            CommandHandlerService.invokeCommandHandler("ID_FLIP_DIMENSION_DIRECTION");

            return Result.Succeeded;
        }
    }
}