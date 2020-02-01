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
    class R0803CreatViewByLevelId : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-3
        /// 展示如何找到所有属于FloorPlan或者CeilingPlan的视图类型,
        /// 然后用这些视图类型,分别创建一个视图,基于一个已有的标高.
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

                Level level=doc.GetElement(new ElementId(341705)) as Level;
                //过滤出所有的ViewFamilyType
                var classFilter = new ElementClassFilter(typeof(ViewFamilyType));
                FilteredElementCollector filteredElements = new FilteredElementCollector(doc);
                filteredElements = filteredElements.WherePasses(classFilter);

                foreach (ViewFamilyType viewFamilyType in filteredElements)
                {
                    //找到ViewFamily类型是FloorPlan或者CeilingPlan的viewFamilyType
                    if (viewFamilyType.ViewFamily == ViewFamily.FloorPlan || viewFamilyType.ViewFamily ==ViewFamily.CeilingPlan)
                    {
                        ViewPlan view =ViewPlan.Create(doc,viewFamilyType.Id,level.Id);
                    }
                }

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