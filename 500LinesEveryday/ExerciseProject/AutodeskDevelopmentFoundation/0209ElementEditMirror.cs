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
    class _0209ElementEditMirror : IExternalCommand

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

                //点选指定执行的元素, 本次按只能选择柱考虑
                Reference pickedEleReference = sel.PickObject(ObjectType.Element, "选择个柱吧");
                //通过引用取到选中的元素
                Element element = doc.GetElement(pickedEleReference);

                FamilyInstance column = doc.GetElement(pickedEleReference) as FamilyInstance;

                string info = "提示如下:";
                info += "\n\t" + "1 族类别ELEM_CATEGORY_PARAM_MT" +
                        element.get_Parameter(BuiltInParameter.ELEM_CATEGORY_PARAM_MT).AsValueString();

                TaskDialog.Show("提示", info);


                if (null != column)
                {
                    Plane plane = Plane.CreateByNormalAndOrigin(XYZ.BasisX, XYZ.Zero);
                    if (ElementTransformUtils.CanMirrorElement(doc, column.Id))
                    {
                        ElementTransformUtils.MirrorElement(doc, column.Id, plane);
                    }

                    TaskDialog.Show("提示", "成功");
                }
                else
                {
                    TaskDialog.Show("提示", "失败");
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