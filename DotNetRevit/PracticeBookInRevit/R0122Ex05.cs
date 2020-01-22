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

            info +="元素公有"+ collector.Count().ToString() +"个\n\t";

            foreach (Element element in collector)
            {
                info += "\nId:" + element.Id.ToString();

                info += "\n     Name:" + element.Name;

                if (null == element.Location)
                {
                    info += "\n     Location: 没有位置";
                }
                else
                {
                    info += "\n     Location:" + element.Location.ToString();
                }

                if (null == element.Category)
                {
                    info += "\n     Category: 没有category";
                }
                else
                {
                    info += "\n Category:" + element.Category.Name;
                }
            }

            TaskDialog.Show("tip", info);

            ts.Commit();

            return Result.Succeeded;
        }
    }
}