using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace RevitDevelopmentFoudation.PracticeBookInRevit
{
    [Transaction(TransactionMode.Manual)]
    class Example1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            var sel = uidoc.Selection;

            var pipe = sel.PickObject(ObjectType.Element).GetElement(doc) as Pipe;

            var conProfile = pipe.ConnectorManager.Connectors.Cast<Connector>().FirstOrDefault().Shape;

            var para =(BuiltInParameter) pipe.LookupParameter("aa").Id.IntegerValue;
            //PostableCommand
            //BuiltInParameter
            //BuiltInCategory
            //ConnectorProfileType
            
            var command = RevitCommandId.LookupPostableCommandId(PostableCommand.Align);
            uiapp.PostCommand(command);

            uiapp.PostCommand(RevitCommandId.LookupCommandId("ID_TOGGLE_ALLOW_DRAG_ON_SELECTION"));
            Enum.GetValues(typeof(PostableCommand));



            return Result.Succeeded;
        }
    }
}
