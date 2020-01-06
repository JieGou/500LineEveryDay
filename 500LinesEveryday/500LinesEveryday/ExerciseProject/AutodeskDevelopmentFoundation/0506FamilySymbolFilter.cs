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
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0506FamilySymbolFilter : IExternalCommand
    {
        /// <summary>
        ///使用FamilySymbolFilter过滤元素
        /// 并获得其中1个族的所有族类型
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
            //famIds 是族Id的集合
            foreach (ElementId famId in famIds)
            {
                collector = new FilteredElementCollector(doc);
                //获得某某族下所有的族类型
                FamilySymbolFilter filter = new FamilySymbolFilter(famId);

                int count = collector.WherePasses(filter).ToElementIds().Count;
                //familySymbols 是族类型id的集合
                ICollection<ElementId> familySymbols = collector.WherePasses(filter).ToElementIds();

                string info2 = null;
                foreach (ElementId symbolId in familySymbols)
                {
                    info2 += "\n\t" + (doc.GetElement(symbolId) as FamilySymbol).Name;
                }

                info += "\n\t" + "\n\t" + "Family(族):" + doc.GetElement(famId).Name
                        + "\n\t" + "■ 族ID是:" + famId.IntegerValue
                        + "\n\t" + "■ FamilySymbols(族类型)分别是:"
                        + info2
                        + "\n\t" + "  共" + count + "个FamilySymbols"
                        + "\n\t" + "■ Category(族分类)是 :" + (doc.GetElement(famId) as Family).FamilyCategory.Name;
                //获得family的id,
                //然后获得族的名称
                //在获得族类型的个数
                //未完成代码:
                //能进一步获得族类型吗?
                //获得族所在category的名称
            }

            TaskDialog.Show("提示", info);
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