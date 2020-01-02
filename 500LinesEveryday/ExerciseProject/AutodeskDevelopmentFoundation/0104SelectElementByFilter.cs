using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0104SelectElementByFilter : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            View acView = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();


                //1 创建收集器
                FilteredElementCollector filteredElements = new FilteredElementCollector(doc);
                //2 创建一个过滤器
                ElementClassFilter classFilter = new ElementClassFilter(typeof(Wall));
                //接着调用收集器的WherePasses函数对元素进行过滤
                filteredElements.WherePasses(classFilter);

                string info = "所选元素为: ";
                foreach (var wall in filteredElements)
                {
                    info += "\n\t" + wall.GetTypeId().ToString();
                   
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