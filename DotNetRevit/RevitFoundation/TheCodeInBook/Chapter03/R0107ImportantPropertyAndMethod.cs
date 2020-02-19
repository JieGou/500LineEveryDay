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

using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0107ImportantPropertyAndMethod : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acView = uidoc.ActiveView;

            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();
                //重要的属性: category类别, location, levelId标高, group组, id, uniqueId唯一id
                //重要的方法: get.materials,
                //通过点选,选择一个元素
                Reference pickedEleReference = sel.PickObject(ObjectType.Element);
                //通过引用,选取到选中的元素.  (群里大佬指点, 用reference是正常方法)
                Element elem = doc.GetElement(pickedEleReference);
                if (elem == null)
                {
                    TaskDialog.Show("提示", "啥也没有,重选");
                }
                var categoryName = elem.Category.Name;
                Location location = elem.Location;
                var levelId = elem.LevelId;
                var group = elem.GroupId;
                var id = elem.Id;
                var uniqueId = elem.UniqueId;
                string info = "选择的元素属性如下";
                info += "\n\t" + "category:" + categoryName;
                info += "\n\t" + "location:" + location;
                info +=  "\n\t" + "levelId:" + levelId;
                info += "\n\t" + "group:" +group;
                info += "\n\t" + "id:" + id;
                info += "\n\t" + "uniqueId:" + uniqueId;
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