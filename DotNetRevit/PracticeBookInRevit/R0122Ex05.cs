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
            BuiltInParameter testParam = BuiltInParameter.ID_PARAM;
            ParameterValueProvider pvp = new ParameterValueProvider(new ElementId((int)testParam));
            FilterNumericRuleEvaluator fnrv = new FilterNumericGreater();
            ElementId ruleValId = new ElementId(0);
            FilterRule fRule = new FilterElementIdRule(pvp, fnrv, ruleValId);
            ElementParameterFilter filter = new ElementParameterFilter(fRule);

            var founds = collector.WherePasses(filter);//引用R0122Ex01的代码.
            IList<string>[,] list =new List<string>[,];

           


            MessageBox.Show(info);

            ts.Commit();

            return Result.Succeeded;
        }
    }
}