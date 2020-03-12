using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Helpers;

namespace CodeInTangsengjiewa2.通用
{
    /// <summary>
    /// 显示族文件的各个视图里的隐藏元素
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_ForcedDisplay : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            var acView = doc.ActiveView;
            Transaction ts = new Transaction(doc, "******");

            try
            {
                ts.Start();

                FilteredElementCollector collection = new FilteredElementCollector(doc);
                collection.WhereElementIsNotElementType();
                var list = new List<ElementId>();

                foreach (Element i in collection)
                {
                    // Selection sel = uidoc.Selection;

                    list.Add(i.Id);
                }

                MessageBox.Show((list.Count.ToString()));
                acView.UnhideElements(list);

                ts.Commit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }

            return Result.Succeeded;
        }
    }
}