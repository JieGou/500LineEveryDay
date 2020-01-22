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

namespace ExerciseProject
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class R0122Ex05 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();
            Transaction ts = new Transaction(doc, "**");

            ts.Start();

            /// 1月22日, 练习05:
            /// 过滤出当文件所有的元素。输出每个元素的ID ，Category，名称， 位置
            ///（分别在项目文档 和 族 文档测试）

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            // ElementClassFilter instanceFilter = new ElementClassFilter(typeof(FamilyInstance));
            // ElementClassFilter hostFilter = new ElementClassFilter(typeof(HostObject));
            // LogicalOrFilter orFilter = new LogicalOrFilter(instanceFilter, hostFilter);
            // collector.WherePasses(orFilter);

            collector.WhereElementIsNotElementType();

            string info = null;

            var list = new List<ElementId>();

            foreach (Element element in collector)
            {
                list.Add(element.Id);
            }

            foreach (var i in list)
            {
                info += "\n Id:" + i.IntegerValue.ToString();
                // info += "\n Category:" + doc.GetElement(i).Category.Name;
                info += "\n Name:" + doc.GetElement(i).Name;
                // info += "\n Location:" + doc.GetElement(i).Location.);
            }

            TaskDialog.Show("tip", info);

            ts.Commit();

            return Result.Succeeded;
        }
    }
}