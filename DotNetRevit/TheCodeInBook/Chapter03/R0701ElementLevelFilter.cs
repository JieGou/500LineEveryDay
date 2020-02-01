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

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0701ElementLevelFilter : IExternalCommand
    {
        /// <summary>
        /// 代码片段3-42
        /// 使用ElementLevelFilter过滤元素
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        void TestElementLevelFilter(Document doc)
        {
            //找到当前标高对应的所有元素
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<ElementId> levelIds = collector.OfClass(typeof(Level)).ToElementIds();
            string info = null;

            foreach (ElementId levelId in levelIds)
            {
                collector = new FilteredElementCollector(doc);
                ElementLevelFilter filter = new ElementLevelFilter(levelId);
                ICollection<ElementId> founds = collector.WherePasses(filter).ToElementIds();

                info += "\n\t" + founds.Count;
                info += "个元素与 Level " + levelId.IntegerValue + "关联";
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

                TestElementLevelFilter(doc);

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