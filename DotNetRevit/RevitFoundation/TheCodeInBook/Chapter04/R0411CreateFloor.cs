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
    class R0411CreateFloor : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-11
        /// 创建楼板
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

                CurveArray curveArray = new CurveArray();
                curveArray.Append(Line.CreateBound(new XYZ(0, 0, 0), new XYZ(100, 0, 0)));
                curveArray.Append(Line.CreateBound(new XYZ(100, 0, 0), new XYZ(0, 100, 0)));
                curveArray.Append(Line.CreateBound(new XYZ(0, 100, 0), new XYZ(0, 0, 0)));
                Floor floor = doc.Create.NewFloor(curveArray, false);

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