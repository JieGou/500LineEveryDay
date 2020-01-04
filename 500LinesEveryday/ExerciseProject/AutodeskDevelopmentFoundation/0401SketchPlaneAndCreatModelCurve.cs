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
    class _0401SketchPlaneAndCreatModelCurve : IExternalCommand
    {
        /// <summary>
        /// 手动建立平面,然后建立工作平面; 
        /// 然后用几何点(0,0,0)到点(10,10,0)的直线创建一条模型曲线
        /// 好像不能创建标高不在工作平面上的模型线
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
            UIView acuiview = uidoc.ActiveUiview();

            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();


                Line line = Line.CreateBound(XYZ.Zero, new XYZ(60000 / 304.8, 60000 / 304.8, 0));
                TaskDialog.Show("提示", "直线创建成功");
                
                Plane plane = Plane.CreateByOriginAndBasis(XYZ.Zero, XYZ.BasisX, XYZ.BasisY);
                TaskDialog.Show("提示", "平面创建成功");
                
                SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
                TaskDialog.Show("提示", "工作平面创建成功");
                
                ModelCurve modelLine = doc.Create.NewModelCurve(line, sketchPlane);
                TaskDialog.Show("提示", "草图上的模型线创建成功");

                #region

                Arc arc = Arc.Create(plane, 5000 / 304.8, 0, Math.PI * 2);
                TaskDialog.Show("提示", "弧创建成功");
                //书上的doc.FamilyCreate.NewModelCurve,已经不能运行.
                //修改为doc.Create.NewModelCurve, 能正确运行.
                //ModelCurve modelCircle = doc.FamilyCreate.NewModelCurve(arc, sketchPlane);
                ModelCurve modelCircle = doc.Create.NewModelCurve(arc, sketchPlane);
                TaskDialog.Show("提示", "模型线创建成功");

                #endregion

                #region 注释的这段代码不能创建模型线, 猜测原因是直线不在工作平面上.

                //  Line line2 = Line.CreateBound(XYZ.Zero, new XYZ(60000 / 304.8, 60000 / 304.8, 3000/304.8));
                //  TaskDialog.Show("提示", "与工作平面不在一个标高的直线创建成功");
                //  ModelCurve modelLine2 = doc.Create.NewModelCurve(line2, modelSketch);
                //  TaskDialog.Show("提示", "与工作平面不在一个标高的模型线创建成功");

                #endregion

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