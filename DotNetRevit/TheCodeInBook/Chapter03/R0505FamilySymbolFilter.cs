using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0505FamilySymbolFilter : IExternalCommand
    {
        /// <summary>
        ///使用FamilySymbolFilter过滤元素
        /// 代码片段3-40
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        ///
        void TestFamilySymbolFilter(Document doc)
        {
            //找到当前文档中族实例所对应的族类型
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<ElementId> famIds = collector.OfClass(typeof(Family)).ToElementIds();
            string info = null;
            foreach (ElementId famId in famIds)
            {
                collector = new FilteredElementCollector(doc);
                FamilySymbolFilter filter = new FamilySymbolFilter(famId);
                int count = collector.WherePasses(filter).ToElementIds().Count;

                info += "\n\t" +
                       "族(Family):"+ doc.GetElement(famId).Name + "(" + "族ID:" + famId.IntegerValue + ")" +
                        " 有" + count + "个FamilySymbols(族类型)  " +
                        " 它的族分类是 :" + (doc.GetElement(famId) as Family).FamilyCategory.Name;
                //获得family的id,
                //然后获得族的名称
                //在获得族类型的个数
                //未完成代码:
                //能进一步获得族类型吗?
                //获得族所在category的名称
            }
            TaskDialog.Show("提示",info );
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

                TestFamilySymbolFilter(doc);

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