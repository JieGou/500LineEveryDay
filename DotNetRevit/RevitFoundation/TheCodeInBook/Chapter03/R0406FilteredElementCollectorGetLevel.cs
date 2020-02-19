using System;
using System.Collections.Generic;
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
    class _0406FilteredElementCollectorGetLevel : IExternalCommand
    {
        /// <summary>
        /// 实现过滤文档中所有的 标高
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
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

                //1 创建收集器
                FilteredElementCollector collection = new FilteredElementCollector(doc);

                //2 接着调用收集器的WherePasses函数和OfClass对元素进行过滤
                //链式调用过滤器
                collection.WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Levels))
                    .WhereElementIsNotElementType();

                string info = "所选元素为: ";

                foreach (Level level in collection)
                {
                    info += "\n\t" + "level.Name : " + level.Name;
                    info += "\n\t" + "level.LevelId : " + level.LevelId;
                    info += "\n\t" + "level.Location : " + level.Location;
                    info += "\n\t" + "level.Id : " + level.Id;
                    info += "\n\t" + "level.Parameters : " + level.LookupParameter("类别").AsValueString();
                    
                    info += "\n\t" + "level.Parameters : " + level.Parameters;
                    info += "\n\t" + "level.ParametersMap : " + level.ParametersMap;
                    info += "\n\t";

                }

                TaskDialog.Show("提示", info);

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