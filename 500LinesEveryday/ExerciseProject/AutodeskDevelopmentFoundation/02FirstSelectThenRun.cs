using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using teacherTangClass;
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _02FirstSelectThenRun : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            View acView = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                //获取当前选择的id
                var collection = sel.GetElementIds();
                if (0 == collection.Count)
                {
                    //如果执行该例子之前没有选择任何元素, 则会弹出提示
                    TaskDialog.Show("工具集提示", "您为选择任何元素");
                }
                else
                {
                    string info = "您选择的元素为:";
                    foreach (var elem in collection)
                    {
                        info += "\n\t" + elem.GetType().ToString();
                    }

                    //显示选择的元素
                    TaskDialog.Show("工具集提示", info);
                }

                ts.Commit();
            }

            catch (Exception)
            {
                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }

            return Result.Succeeded;
        }
    }
}