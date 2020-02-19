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

using View = Autodesk.Revit.DB.View;
using MyClass;
using Form = Autodesk.Revit.DB.Form;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0703ElementInstanceFilter : IExternalCommand
    {
        /// <summary>
        /// 代码片段3-43
        /// 使用ElemeterInstanceFilter过滤元素
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        void TestElementInstanceFilter(Document doc)
        {
            //找到名字为 “xxx” 的族类？
            string targerName = "1000 x 1000mm";


            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector = collector.OfClass(typeof(FamilySymbol));
            //过滤不了系统族
            var query = from element in collector
                where element.Name == targerName
                select element; //LINQ 查询
            List<Autodesk.Revit.DB.Element> familySymbols = query.ToList<Autodesk.Revit.DB.Element>();
            TaskDialog.Show("tips", familySymbols.Count.ToString());
            ElementId symbolId = familySymbols[0].Id;

            //创建过滤器并找到族类型对应的族实例
            collector = new FilteredElementCollector(doc);
            FamilyInstanceFilter filter = new FamilyInstanceFilter(doc, symbolId);
            ICollection<Element> founds = collector.WherePasses(filter).ToElements();
            foreach (FamilyInstance insta in founds)
            {
                string info = null;
                info += "FamilyInstance:" + insta.Id.IntegerValue
                                          + "\nFamilySymbol Id:" + insta.Symbol.Id.IntegerValue
                                          + "\nName: " + insta.Symbol.Name;
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



            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                TestElementInstanceFilter(doc);

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