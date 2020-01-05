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
    class _0501ElementCategoryFilter : IExternalCommand
    {
        /// <summary>
        ///实现收集文档中虽有基本墙 类别下的所有 type
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

                //创建收集器
                FilteredElementCollector collector = new FilteredElementCollector(doc);

                //创建过滤器
                ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

                ICollection<Element> founds = collector.WherePasses(filter).ToElements();

                string info = null;

                foreach (Element elem in founds)
                {
                    //  Trace.WriteLine(String.Format("Element id:{0},type :{1}",elem.Id.IntegerValue, elem.GetType().Name));
                    info += "\n\t" + "Element id : " + elem.Id.IntegerValue;
                    info += "\n\t" + "Name : " + elem.Name;
                    info += "\n\t" + "type : " + elem.GetType().Name;
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