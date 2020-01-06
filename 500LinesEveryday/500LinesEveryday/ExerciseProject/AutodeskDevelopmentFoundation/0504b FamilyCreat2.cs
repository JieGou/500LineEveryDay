using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// 使用API来编辑族时, 使用doc.Family.Creat.NewReferencePlan();创建参考平面
    /// </summary>
    /// 
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0504FamilyCreat2 : IExternalCommand
    {
        /// <summary>
        ///用来匹配元素类型的过滤器
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>

        // //书上3-32 第67页
        // public void CreatSketchPlaneByPlane()
        // {
        //     Document doc = this.ActiveUIDocument.Document;
        //     using (Transaction trans = new Transaction(doc, "Creat model arc"))
        //     {
        //         trans.Start();
        //         Plane plane = this.Application.Creat.NewPlane(XYZ.BasisZ, XYZ.Zero);
        //         SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
        //
        //         Arc arc = Arc.Create(plane, 5, 0, Math.PI * 2);
        //         ModelCurve modelCircle = doc.FamilyCreate.NewModelCurve(arc, sketchPlane);
        //         trans.Commit();
        //     }
        // }


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;
            Selection sel = app.ActiveUIDocument.Selection;


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                CreatSketchPlaneByPlane();

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