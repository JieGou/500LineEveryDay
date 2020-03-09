using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CodeInTangsengjiewa3.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CreateAGroupOfType : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Autodesk.Revit.DB.Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            View acview = uidoc.ActiveView;

            TaskDialog.Show("tip", "hello");

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var symbols = collector.OfClass(typeof(FamilySymbol)).Where(m => m.Name.Contains("475 x 610mm"));

            var targetsymbol = symbols.First() as FamilySymbol;

            //var newsymbolName = "500 x 600mm";

            Transaction ts = new Transaction(doc, "fuzhileixing");
            ts.Start();

            for (int i = 100; i < 1000; i += 100)
            {
                var newsymbolName = $"HW{i} x {i}";
                if (!symbols.Select(m => m.Name).Contains(newsymbolName))
                {
                    var newtype = targetsymbol.Duplicate(newsymbolName);

                    newtype.LookupParameter("深度").Set(i / 304.8);
                    newtype.LookupParameter("偏移基准").Set(0 / 304.8);
                    newtype.LookupParameter("偏移顶部").Set(0 / 304.8);
                    newtype.LookupParameter("宽度").Set(i / 304.8);

                    TaskDialog.Show("tip", "创建完成!");
                }
                else
                {
                    TaskDialog.Show("tip", "已经包含,不创建!");
                }
            }

            ts.Commit();

            return Result.Succeeded;
        }
    }
}