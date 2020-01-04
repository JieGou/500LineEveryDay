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


                FilteredElementCollector collection = new FilteredElementCollector(doc);
                ElementFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StackedWalls);
                collection.OfClass(typeof(Wall)).WherePasses(filter);
                ICollection<ElementId> foundIds = collection.ToElementIds();


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