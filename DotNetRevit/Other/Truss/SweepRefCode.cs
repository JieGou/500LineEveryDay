using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Structure;


namespace Truss
{
    [Journaling(JournalingMode.NoCommandData)]
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class SweepRefCode : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document revitDoc = commandData.Application.ActiveUIDocument.Document;//获取文档
            Application revitApp = commandData.Application.Application;//获取应用程序
            CreatSweepSolid(revitDoc);
            return Result.Succeeded;
        }

        public void CreatSweepSolid(Document revitDoc)
        {
            //获取模型线列表
            FilteredElementCollector modelCurveCollector = new FilteredElementCollector(revitDoc);
            List<ModelCurve> modelCurveList =
                modelCurveCollector.OfCategory(BuiltInCategory.OST_Lines).OfClass(typeof(CurveElement)).ToList()
                .ConvertAll(x => x as ModelCurve);//注意此处的typeof(CurveElement)，不能用ModelCurve，会报错
            foreach (ModelCurve modelCurve in modelCurveList)
            {
                //第一步，创建一个新的族文件
                Document familyDoc = revitDoc.Application.NewFamilyDocument
                    (@"C:\ProgramData\Autodesk\RVT 2019\Family Templates\Chinese\公制常规模型.rft");
                //现在就是族环境了，需要对族进行操作在此处进行
                //获取pathList,得到扫略路径 
                /*  对于项目环境中的模型线，不可以直接引用。需要根据已有模型线，在族环境中重新创建
                ReferenceArray path = new ReferenceArray();
                Reference reference = modelCurve.GeometryCurve.Reference;
                path.Append(reference);
                */
                //定义扫略截面SweepProfile,此处无需生成模型线的！直接用Curve即可
                Line line1 = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(40 / 304.8, 0, 0));
                Line line2 = Line.CreateBound(new XYZ(40 / 304.8, 0, 0), new XYZ(40 / 304.8, 6 / 304.8, 0));
                Line line3 = Line.CreateBound(new XYZ(40 / 304.8, 6 / 304.8, 0), new XYZ(0, 6 / 304.8, 0));
                Line line4 = Line.CreateBound(new XYZ(0, 6 / 304.8, 0), new XYZ(0, 0, 0));
                CurveArray curveArray = new CurveArray();
                curveArray.Append(line1);
                curveArray.Append(line2);
                curveArray.Append(line3);
                curveArray.Append(line4);
                CurveArrArray curveArrArray = new CurveArrArray();
                curveArrArray.Append(curveArray);
                SweepProfile sweepProfile = familyDoc.Application.Create.NewCurveLoopsProfile(curveArrArray);
                Transaction transaction1 = new Transaction(familyDoc, "SweepProfile");//注意这里的Document用的是族doc,而不是revitDoc                
                transaction1.Start();
                //在族环境中，重新创建草图平面、模型线
                Plane plane = modelCurve.SketchPlane.GetPlane();
                SketchPlane sketchPlane = SketchPlane.Create(familyDoc, plane);
                ModelCurve modelCurvePath = familyDoc.FamilyCreate.NewModelCurve(modelCurve.GeometryCurve, sketchPlane);
                /*
                SketchPlane sketchPlane = modelCurve.SketchPlane;
                ModelCurve modelCurvePath = familyDoc.FamilyCreate.NewModelCurve(modelCurve.GeometryCurve, sketchPlane);             
                */
                //定义扫略路径path
                ReferenceArray path = new ReferenceArray();
                path.Append(modelCurvePath.GeometryCurve.Reference);
                //创建扫略放样
                familyDoc.FamilyCreate.NewSweep(true, path, sweepProfile, 0, ProfilePlaneLocation.Start);
                transaction1.Commit();
                //对族的编辑完成后，加载族文件到项目文件
                Family loadingFamily = familyDoc.LoadFamily(revitDoc);
                //在项目中实例化该族
                FamilySymbol familySymbol = revitDoc.GetElement(loadingFamily.GetFamilySymbolIds().First()) as FamilySymbol;
                Transaction transaction2 = new Transaction(revitDoc, "CreatFamilyInstance");
                transaction2.Start();
                familySymbol.Activate();//必须要激活
                revitDoc.Create.NewFamilyInstance(XYZ.Zero, familySymbol, StructuralType.NonStructural);
                transaction2.Commit();
            }
        }
        public class ProjectFamLoadOption : IFamilyLoadOptions//项目中创建族，需要用到此接口
        {
            public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
            {
                overwriteParameterValues = true;
                return true;
            }

            public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
            {
                source = FamilySource.Project;
                overwriteParameterValues = true;
                return true;
            }
        }
    }
}