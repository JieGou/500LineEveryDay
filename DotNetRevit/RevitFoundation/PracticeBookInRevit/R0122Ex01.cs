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
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject.PracticeBookInRevit
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class R0122Ex01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;

            Transaction ts = new Transaction(doc, "**");

            ts.Start();

            /// 1月22日, 练习01:
            /// 在revit中过滤出名称为 标准 的 元素个数

            string info = null;
            
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            // info += collector.Count().ToString() + "\n";  //提示没有过滤器前.取出或者迭代都不允许

            // collector.WhereElementIsNotElementType(); //这是什么意思啊.  为什么至少要一个过滤器?

            //找到元素Id大于0的元素 //为什么至少要一个过滤器?
            BuiltInParameter testParam = BuiltInParameter.ID_PARAM;
            ParameterValueProvider pvp = new ParameterValueProvider(new ElementId((int) testParam));
            FilterNumericRuleEvaluator fnrv = new FilterNumericGreater();
            ElementId ruleValId = new ElementId(0);
            FilterRule fRule = new FilterElementIdRule(pvp, fnrv, ruleValId);
            ElementParameterFilter filter = new ElementParameterFilter(fRule);

            var founds = collector.WherePasses(filter);

            var targetElements = from element in founds
                where element.Name == "标准"
                select element;

            List<Element> bzElements = targetElements.ToList<Element>();

            info = "名称为 标准 的元素数量是: " + bzElements.Count.ToString();

            foreach (Element element in bzElements)
            {
                info += "\n\t Id:" + element.Id + ";Name:" + element.Name + "\n\t";
            }

            MessageBox.Show(info);

            ts.Commit();

            return Result.Succeeded;
        }
    }
}