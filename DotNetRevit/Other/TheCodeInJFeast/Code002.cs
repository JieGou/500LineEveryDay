using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using View = Autodesk.Revit.DB.View;


namespace TheCodeInJFeast
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    /* https://www.bilibili.com/video/av60526429?p=5              
    */
    class Code002 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            // collector.OfClass(typeof(Wall));

            // collector.OfCategory(BuiltInCategory.OST_DuctCurves);

            collector.WherePasses(new ElementClassFilter(typeof(Wall)));

            collector.WherePasses(new ElementClassFilter(typeof(Duct)));

            Transaction ts = new Transaction(doc, "**");

            ts.Start();

            ts.Commit();
            return Result.Succeeded;
        }
    }
}