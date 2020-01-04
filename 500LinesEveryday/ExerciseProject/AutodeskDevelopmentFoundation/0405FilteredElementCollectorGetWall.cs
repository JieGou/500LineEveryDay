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
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0405FilteredElementCollector : IExternalCommand
    {
        /// <summary>
        ///0405FilteredElementCollector
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
            UIView acuiview = uidoc.ActiveUiview();


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                //1 创建收集器
                FilteredElementCollector collection = new FilteredElementCollector(doc);

                //2 创建一个过滤器
               // ElementFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StackedWalls);

                //接着调用收集器的WherePasses函数和OfClass对元素进行过滤
                //链式调用过滤器
                collection.OfClass(typeof(Wall));
                    //.WherePasses(filter);
                ICollection<ElementId> foundIds = collection.ToElementIds();

                string info = "所选元素为: ";
                foreach (var wall in collection)
                {
                    info += "\n\t族类型Id:" + wall.GetTypeId().ToString()+"\n\t元素Id:"+wall.Id.ToString();
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