using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassTeacherXu.Helpers;
using View = Autodesk.Revit.DB.View;
using MyClass;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0702ElementParameterFilter : IExternalCommand
    {
        /// <summary>
        /// 代码片段3-43
        /// 使用ElemeterParameterFilter过滤元素
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        void TestElementParameterFilter(Document doc)
        {
            //找到所有id大于99的元素
            BuiltInParameter testPara = BuiltInParameter.ID_PARAM;
            //提供者
            ParameterValueProvider pvp = new ParameterValueProvider(new ElementId((int)testPara));

            //评估者
            FilterNumericRuleEvaluator fnrv = new FilterNumericGreater();

            //规则者
            ElementId ruleValId = new ElementId(99999); //Id大于99, 由于文件里的元素太多， 运行后能按一年确定不带停的。

            //创建规则过滤器和对应的元素过滤器
            FilterRule fRule = new FilterElementIdRule(pvp, fnrv, ruleValId);
            ElementParameterFilter filter = new ElementParameterFilter(fRule);
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> founds = collector.WherePasses(filter).ToElements();
            foreach (Element elem in founds)
            {
                string info = null;
                info += "Element id :" + elem.Id.IntegerValue;
                TaskDialog.Show("tips", info);

            }

        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                TestElementParameterFilter(doc);

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