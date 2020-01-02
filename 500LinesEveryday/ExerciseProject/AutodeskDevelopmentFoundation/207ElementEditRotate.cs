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
    class _207ElementEditRotate : IExternalCommand

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

                //获取一堵墙,并创建一条和墙位置垂直的旋转轴,然后对这堵墙进行逆时针60°的旋转

                //点选指定执行的元素, 本次按只能选择柱考虑
                Reference pickedEleReference = sel.PickObject(ObjectType.Element, "选择个墙吧");
                //通过引用取到选中的元素
                Wall wall = doc.GetElement(pickedEleReference) as Wall;

                LocationCurve wallLine = wall.Location as LocationCurve;
                XYZ point1 = wallLine.Curve.GetEndPoint(0);
                XYZ point2 = new XYZ(point1.X, point1.Y, 30);

                Line axis = Line.CreateBound(point1, point2);

                ElementTransformUtils.RotateElement(doc, wall.Id, axis, Math.PI / (180 / 60));

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