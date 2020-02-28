using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using View = Autodesk.Revit.DB.View;

namespace RevitDevelopmentFoundation.Chapter04
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0412ExtrusionRoof : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-12 创建屋面 ExtrusionRoof
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acView = uidoc.ActiveView;

            Transaction ts = new Transaction(doc, "******");

            try
            {
                ts.Start();

                //先创建参照平面
                XYZ bubbleEnd =new XYZ(0,0,0);
                XYZ freeEnd =new XYZ(0,100,0);
                XYZ thirdPnt =new XYZ(0,0,100);
                ReferencePlane plane = doc.Create.NewReferencePlane2(bubbleEnd, freeEnd, thirdPnt, acView);

                //创建屋顶前准备参数
                Level level = doc.GetElement(new ElementId(341705)) as Level;
                RoofType roofType =doc.GetElement(new ElementId(335)) as RoofType;
                CurveArray currArray=new CurveArray();
                currArray.Append(Line.CreateBound(new XYZ(0, 0, 50), new XYZ(0, 50, 100)));
                currArray.Append(Line.CreateBound(new XYZ(0,50,100),new XYZ(0,100,50) ));
                //创建屋顶
                doc.Create.NewExtrusionRoof(currArray, plane, level, roofType, 1000/304.8, 2000/304.8);


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