using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using UIFrameworkServices;

namespace CodeInTangsengjiewa3.高级
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CallSystemCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = doc.ActiveView;

            var ele = sel.PickObject(ObjectType.Element, "请选择一个剖面符号");
            sel.SetElementIds(new List<ElementId>() {ele.ElementId});
            CommandHandlerService.invokeCommandHandler("ID_SECTION_GAP");
            sel.SetElementIds(new List<ElementId>()); //清空当前选择的内容;
            return Result.Succeeded;
        }
    }
}