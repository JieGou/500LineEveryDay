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
using ViewSection = Autodesk.Revit.DB.ViewSection;

namespace CodeInTangsengjiewa2.高级
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CallSystemCmd:IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var sel = uidoc.Selection;
            var doc = uidoc.Document;
            var acview = doc.ActiveView as ViewSection;//???

            var ele = sel.PickObject(ObjectType.Element);

            //选中剖面符号
            sel.SetElementIds(new List<ElementId>() { ele.ElementId });

            CommandHandlerService.invokeCommandHandler("ID_SECTION_GAP");

            sel.SetElementIds(new List<ElementId>());//清空到当前没有选择的状态.

            return Result.Succeeded;
        }
    }
}
