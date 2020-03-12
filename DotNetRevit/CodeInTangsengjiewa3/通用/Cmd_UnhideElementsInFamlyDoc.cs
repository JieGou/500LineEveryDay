using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa3.BinLibrary.Helpers;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace CodeInTangsengjiewa3.通用
{
    /// <summary>
    ///  show hided elements in family document of any view
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_UnhideElementsInFamlyDoc : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Application app = commandData.Application.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            View acView = uidoc.ActiveView;

            if (!doc.IsFamilyDocument)
            {
                MessageBox.Show("请在族文档中使用该命令.");
            }
            var views = doc.TCollector<View>().Where(m => !(m.IsTemplate));
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            var elementList = collector.WhereElementIsNotElementType();

            Transaction ts = new Transaction(doc, "显示族的隐藏元素");
            try
            {
                ts.Start();
                foreach (var view in views)
                {
                    if (view is ViewPlan || view is ViewSection || view is View3D)
                    {
                        foreach (var item in elementList)
                        {
                            if (item.IsHidden((view)))
                            {
                                view.UnhideElements(new List<ElementId>() {item.Id});
                            }
                        }
                    }
                }
                ts.Commit();
            }
            catch (Exception e)
            {
                message = e.ToString();
                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }
            return Result.Succeeded;
        }
    }
}