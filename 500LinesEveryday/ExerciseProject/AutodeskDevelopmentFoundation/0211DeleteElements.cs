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
    class _0211DeleteElements : IExternalCommand
    {
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
                List<ElementId> elementsToDelete = new List<ElementId>();
                //uidoc.Selection.PickElementsByRectangle() 是一个让用户用鼠标画矩形框选择的 方法
                string info = "删除的元素如下:";
                foreach (var element in uidoc.Selection.PickElementsByRectangle())
                {
                    elementsToDelete.Add(element.Id);
                    info +="\n\t"+ element.Id;
                }
                ICollection<ElementId> deletedElements = doc.Delete(elementsToDelete);
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