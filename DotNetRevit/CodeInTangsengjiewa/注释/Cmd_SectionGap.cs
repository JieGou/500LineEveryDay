using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using CodeInTangsengjiewa.BinLibrary.Extensions;
using UIFrameworkServices;


namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.注释
{
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_SectionGap : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Selection sel = uidoc.Selection;

            var eleId = sel.PickObject(ObjectType.Element,
                                       uidoc.Document.GetSelectionFilter(m =>
                                       {
                                           return m.Category.Id ==
                                                  new ElementId(BuiltInCategory
                                                                    .OST_Viewers);
                                       })).ElementId;
            string commandId = "ID_SECTION_GAP";

            sel.SetElementIds(new List<ElementId>() {eleId});

            CommandHandlerService.invokeCommandHandler(commandId);
            return Result.Succeeded;
        }
    }
}