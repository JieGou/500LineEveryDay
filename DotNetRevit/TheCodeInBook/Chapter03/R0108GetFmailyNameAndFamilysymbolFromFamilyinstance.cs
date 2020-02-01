using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using Autodesk.Revit.DB.Events;

using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0108GetFmailyNameAndFamilysymbolFromFamilyinstance : IExternalCommand
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
                FamilyInstance familyInstance = elem as FamilyInstance;
                string family = familyInstance.Category.Name;
                string familySymbol = familyInstance.Name;
                string info = "选择的元素属性如下";
                info += "\n\t" + "族名称是:" + family;
                info += "\n\t" + "族类型名称是:" + familySymbol;
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