using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_IndependentTag : IExternalCommand
    {
        /// <summary>
        /// 过滤一张视图上的independenttag  条件是 tag标记的元素名称包含 “标准” 两个字
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            Regex newRegex = new Regex("标准");

            List<Element> collector = new FilteredElementCollector(doc).WhereElementIsNotElementType()
                .OfClass(typeof(IndependentTag))
                .Where(m => m.Name.Regex("标准") && m.OwnerViewId==doc.ActiveView.Id).ToList();

            List<ElementId> collectorIds = collector.Select(m => m.Id).ToList();
            string info = "";
            foreach (Element element in collector)
            {
                info += element.Id + "\n";
            }
            info += "总共有 " + collector.Count + "个元素符合.";
            MessageBox.Show(info);
            sel.SetElementIds(collectorIds);
            return Result.Succeeded;
        }
    }
}

public static class temUtis
{
    public static bool Regex(this string name, string key)
    {
        bool result = false;
        Regex regex = new Regex(key);
        return result = System.Text.RegularExpressions.Regex.IsMatch(name, key);
    }
}