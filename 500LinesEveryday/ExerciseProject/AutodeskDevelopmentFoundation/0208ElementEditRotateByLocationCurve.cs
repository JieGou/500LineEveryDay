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
    class _0208ElementEditRotateByLocationCurve : IExternalCommand

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

                ///通过LocationCurve或LocationPoint旋转元素

                //点选指定执行的元素, 本次按只能选择柱考虑
                Reference pickedEleReference = sel.PickObject(ObjectType.Element, "选择个墙吧");
                //通过引用取到选中的元素
                Element element = doc.GetElement(pickedEleReference);

                string info = "提示如下:";
                info += "\n\t" + "1 族类别ELEM_CATEGORY_PARAM_MT" +
                        element.get_Parameter(BuiltInParameter.ELEM_CATEGORY_PARAM_MT).AsValueString();
                info += "\n\t" + "2 族类别ELEM_CATEGORY_PARAM" +
                        element.get_Parameter(BuiltInParameter.ELEM_CATEGORY_PARAM).AsValueString();
                TaskDialog.Show("提示", info);

                XYZ point1 = XYZ.Zero;
                XYZ point2 = XYZ.Zero;

                //当选择的对象是墙时:通过元素的位置来旋转元素
                if (element.get_Parameter(BuiltInParameter.ELEM_CATEGORY_PARAM_MT).AsValueString() == "墙")
                {
                    Wall wall = doc.GetElement(pickedEleReference) as Wall;
                    LocationCurve curve = wall.Location as LocationCurve;

                    Curve line = curve.Curve;
                    point1 = line.GetEndPoint(0);
                    point2 = new XYZ(point1.X, point1.Y, point1.Z + 10);

                    Line axis = Line.CreateBound(point1, point2);
                    curve.Rotate(axis, Math.PI / (180 / 30));
                }

                //当选择的对象是柱时:通过元素的位置点来旋转元素
                else if (element.get_Parameter(BuiltInParameter.ELEM_CATEGORY_PARAM_MT).AsValueString() == "柱")
                {
                    LocationPoint point = element.Location as LocationPoint;
                    if (null != point)
                    {
                        point1 = point.Point;
                        point2 = new XYZ(point1.X, point1.Y, point1.Z + 10);
                        Line axis = Line.CreateBound(point1, point2);
                        point.Rotate(axis, Math.PI / (180 / 30));
                    }
                }

                //当选择的对象是不是墙也不是柱时, 提示干不了.
                else
                {
                    TaskDialog.Show("提示", "哥,既不是墙,也不是柱,我没法弄啊");
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