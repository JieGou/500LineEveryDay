using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Windows;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    /// <summary>
    /// what can i do with revit api now?
    /// 尝试获取viewfamilytypes
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_GetViewFamilyTypes : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            Transaction ts = new Transaction(doc, "创建3d视图");
            ts.Start();

            var collector = new FilteredElementCollector(doc)
                .WhereElementIsElementType().OfType<ViewFamilyType>().OrderBy(m => m.FamilyName);

            string info = "";
            foreach (var element in collector)
            {
                info += element.FamilyName + " : " + element.Name + "\n";
            }
            MessageBox.Show(info);

            ts.Commit();

            return Result.Succeeded;
        }
    }
}