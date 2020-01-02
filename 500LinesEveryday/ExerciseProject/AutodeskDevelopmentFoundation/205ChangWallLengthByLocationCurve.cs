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
    class _205ChangWallLengthByLocationCurve : IExternalCommand

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

                if (null != wall)
                {
                    LocationCurve wallLine = wall.Location as LocationCurve;

                    XYZ pointOne = XYZ.Zero;
                    XYZ pointTwo= new XYZ(200,200,0);
                    //定义线
                    Line newWallLine = Line.CreateBound(pointOne, pointTwo);

                    //把墙的位置线换成新的线
                    wallLine.Curve = newWallLine;

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