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

using View = Autodesk.Revit.DB.View;


namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0606ExclusionFilter : IExternalCommand
    {
        /// <summary>
        ///代码片段3-41
        /// 使用ExclusionFilter过滤元素
        /// 使用所有族类型作为排除的集合
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        void TestExclusionFilter(Document doc)
        {
            //找到所有除族类型FamilySymbol外的元素类型 ElementType
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            ICollection<ElementId> elementTypeCollection = collector.WhereElementIsElementType().ToElementIds();

            ICollection<ElementId> familySymbolCollection = collector.OfClass(typeof(FamilySymbol)).ToElementIds();


            //创建一个排除族类型FamilySymbol的过滤器
            ExclusionFilter filter = new ExclusionFilter(familySymbolCollection);
            ICollection<ElementId> founds = collector.WhereElementIsElementType().WherePasses(filter).ToElementIds();
            //未完成代码，ExclusionFilter不能排除元素
            

            string info = "共找到" + elementTypeCollection.Count + "个ElementType\n";
            info += "其中" + familySymbolCollection.Count + "是FamilySymbol\n";
            info += "其中" + founds.Count.ToString() + "个不是FamilySymbol";
            //未完成代码， 过滤得到非familySymbol的数量不一致。
            TaskDialog.Show("tips", info);
            

        }
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;



            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                TestExclusionFilter(doc);

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