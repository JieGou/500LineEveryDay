using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassTeacherXu.Helpers;
using View = Autodesk.Revit.DB.View;

namespace TheCodeInKaiFaZhiNan.Chapter2
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class Code0201 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /* revit开发指南 第二章 代码2-1 */

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();

            //Find all wall instances in the document by using category filter
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            //apply the filter to the elements in the active document
            //use shortcut whereElementIsNotElementType() to find wall instances only

            IList<Element> walls = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            string prompt = "the walls in the current document are: \n";

            foreach (Element e in walls)
            {
                prompt += " 元素id:" + e.Id.IntegerValue + "; 元素名:" + e.Name + "\n";
            }

            TaskDialog.Show("tips", prompt);

            Transaction ts = new Transaction(doc, "**");

            ts.Start();

            ts.Commit();

            return Result.Succeeded;
        }
    }
}