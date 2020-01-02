using System;
using System.Collections.Generic;
using System.Linq;
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
    class _202CreatFamilyTypeAndGetParameter : IExternalCommand

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

                //点选指定执行的元素, 本次按只能选择墙考虑
                Reference pickedEleReference = sel.PickObject(ObjectType.Element);
                //通过引用取到选中的元素
                Wall wall = doc.GetElement(pickedEleReference) as Wall;


                string info = "信息如下";

                info += "\n\t" + "1 ELEM_TYPE_PARAM (族名称):" +
                        wall.get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM).AsValueString();

                info += "\n\t" + "2 长度:" + wall.LookupParameter("长度").AsValueString();

                TaskDialog.Show("提示", info);

                //当新添加的族类型族中已经有的时候, 该程序不能正常运行.
                WallType wallType = wall.WallType;
                ElementType duplicatedWallType = wallType.Duplicate(wallType.Name + "duplicated7");

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