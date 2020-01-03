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
    class _0301CreatGroup : IExternalCommand
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
                List<ElementId> elementsToGroup = new List<ElementId>();

                string info = "成组的元素如下:";
                //uidoc.Selection.PickElementsByRectangle() 是一个让用户用鼠标画矩形框选择的 方法
                // foreach (var element in uidoc.Selection.PickElementsByRectangle())
                // {
                //     elementsToGroup.Add(element.Id);
                //     info += "\n\t" + element.Id;
                // }

                //换一种选择方式
                var referenceCollection = uidoc.Selection.PickObjects
                    (ObjectType.Element, "请选择元素");
                foreach (var reference in referenceCollection)
                {
                    var elem = doc.GetElement(reference);
                    elementsToGroup.Add(elem.Id);
                    info += "\n\t" + "elem.Id: " + elem.Id + "; elem.GetType" + elem.GetType().ToString();
                }
                TaskDialog.Show("提示", info);
                Group group = doc.Create.NewGroup(elementsToGroup);

                //重命名
                group.GroupType.Name = "MyGroup";

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