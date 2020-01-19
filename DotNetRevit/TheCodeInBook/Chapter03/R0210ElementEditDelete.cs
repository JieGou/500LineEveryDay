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
using ClassTeacherXu.Helpers;
using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0210ElementEditDelete : IExternalCommand
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
                //删除因素的练习
                //点选指定执行的元素, 本次按只能选择柱考虑
                Reference pickedEleReference = sel.PickObject(ObjectType.Element, "选择个元素吧");
                //通过引用取到选中的元素
                Element element = doc.GetElement(pickedEleReference);
                string info = "成功删除了: ";
                info += element.Id.ToString();
                ICollection<ElementId> deleterElementIds = doc.Delete(element.Id);
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