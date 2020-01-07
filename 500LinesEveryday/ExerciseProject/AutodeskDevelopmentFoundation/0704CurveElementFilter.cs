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
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;
using MyClass;
using Form = Autodesk.Revit.DB.Form;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0704CurveElementFilter : IExternalCommand
    {
        /// <summary>
        /// 代码片段3-43
        /// 使用CurveElementFilter过滤元素
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        void TestCurveElementFilter(Document doc)
        {
            //找到所有线元素类型对应的线性元素
            Array stTypes = Enum.GetValues(typeof(CurveElementType));
            foreach (CurveElementType tstType in stTypes)
            {
               // if (tstType == CurveElementType.Invalid) continue;
                FilteredElementCollector collector = new FilteredElementCollector(doc);
                CurveElementFilter filter = new CurveElementFilter(tstType);
                int foundNum = collector.WherePasses(filter).ToElementIds().Count;

                string info = tstType.GetType().Name + ": elements amount " + foundNum;

                TaskDialog.Show("tip", info);


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

                TestCurveElementFilter(doc);

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