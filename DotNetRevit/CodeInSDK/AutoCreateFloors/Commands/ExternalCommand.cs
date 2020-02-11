using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Views;


namespace RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Commands
{
    [Transaction(TransactionMode.Manual)]
    class ExternalCommand : IExternalCommand
    {
        public static Document doc = null;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;

            CreateFloorsMainWindow mainWindow = new CreateFloorsMainWindow(commandData);

            mainWindow.ShowDialog();
            return Result.Succeeded;
        }
    }


    //结果框架过滤器
    public class FilteredColumnAndBeams : ISelectionFilter
    {
        private Document doc = ExternalCommand.doc;

        public bool AllowElement(Element elem)
        {
            if (elem.Category.Name == Category.GetCategory(doc, BuiltInCategory.OST_StructuralFraming).Name
                || elem.Category.Name == Category.GetCategory(doc, BuiltInCategory.OST_StructuralColumns).Name)
            {
                return true;
            }

            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}