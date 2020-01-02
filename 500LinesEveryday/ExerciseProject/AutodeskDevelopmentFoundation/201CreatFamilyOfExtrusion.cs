using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using Autodesk.Revit.DB.Events;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _201CreatFamilyOfExtrusion : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            //
            // View acView = uidoc.ActiveView;
            // UIView acuiview = uidoc.ActiveUiview();

            //创建简单的拉升族
            string templateFileName = @"C:\ProgramData\Autodesk\RVT 2019\Family Templates\Chinese\公制常规模型.rft";

            Document familyDoc = commandData.Application.Application.NewFamilyDocument
                (templateFileName);


            using (Transaction ts = new Transaction(familyDoc))

            {
                ts.Start("CreatFamily");


                CurveArray curveArray = new CurveArray();
                curveArray.Append(Line.CreateBound(new XYZ(0, 0, 0), new XYZ(5 / 304.8, 0, 0)));
                curveArray.Append(Line.CreateBound(new XYZ(5 / 304.8, 0, 0), new XYZ(5 / 304.8, 5 / 304.8, 0)));
                curveArray.Append(Line.CreateBound(new XYZ(5 / 304.8, 5 / 304.8, 0), new XYZ(0, 5 / 304.8, 0)));
                curveArray.Append(Line.CreateBound(new XYZ(0, 5 / 304.8, 0), new XYZ(0, 0, 0)));

                CurveArrArray curveArrArray = new CurveArrArray();
                curveArrArray.Append(curveArray);

                Plane plane =
                    Plane.CreateByOriginAndBasis(XYZ.Zero, new XYZ(1 , 0, 0), new XYZ(0, 1 , 0));
                SketchPlane sketchPlane = SketchPlane.Create(familyDoc, plane);

                //创建一个拉拉升体
                familyDoc.FamilyCreate.NewExtrusion
                    (true, curveArrArray, sketchPlane, 10 / 304.8);

                //创建一个族类型
                familyDoc.FamilyManager.NewType("MyNewType");

                ts.Commit();

                familyDoc.SaveAs(@"D:\MyNewFamily3.rfa");
                familyDoc.Close();
            }

            TaskDialog.Show("提示", "创建成功");
            return Result.Succeeded;
        }
    }
}