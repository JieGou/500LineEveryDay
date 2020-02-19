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

using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0403CreatSketchPlanByPlane : IExternalCommand
    {
        /// <summary>
        /// SketchPlaneByLevelId
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();


                //书上的这段代码不能用
                //Plane plane = this.Application.creat.NewPlane(XYZ.BasisZ, XYZ.Zero);

                Plane plane = Plane.CreateByOriginAndBasis(XYZ.Zero, XYZ.BasisX, XYZ.BasisY);

                TaskDialog.Show("提示", "平面创建成功");

                SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
                TaskDialog.Show("提示", "工作平面创建成功");


                Arc arc = Arc.Create(plane, 5000 / 304.8, 0, Math.PI * 2);
                TaskDialog.Show("提示", "弧创建成功");

                ModelCurve modelCircle = doc.FamilyCreate.NewModelCurve(arc, sketchPlane);
                TaskDialog.Show("提示", "模型圆弧创建成功");


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