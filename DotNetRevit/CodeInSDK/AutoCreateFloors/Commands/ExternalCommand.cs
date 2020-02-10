using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;


namespace RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Commands
{
    [Transaction(TransactionMode.Manual)]
    class ExternalCommand:IExternalCommand
    {
        public static Document doc = null;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;

            Create

        }
    }
}
