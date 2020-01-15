using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Myclass;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;
using MyClass;
using Form = Autodesk.Revit.DB.Form;

namespace RevitDevelopmentFoundation.Chapter04
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0414FootPrintRoof : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-14 创建家具
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
                // 创建迹线屋顶前准备参数
                Level level =doc.GetElement(new ElementId(341705)) as Level;
                RoofType roofType =doc.GetElement(new ElementId(337)) as RoofType;
                CurveArray curveArray =new CurveArray();
                //屋顶外边框
                curveArray.Append(Line.CreateBound(new XYZ(0,0,0),new XYZ(30,0,0) ));
                curveArray.Append(Line.CreateBound(new XYZ(30,0,0),new XYZ(30,30,0) ));
                curveArray.Append(Line.CreateBound(new XYZ(30,30,0),new XYZ(0,30,0) ));
                curveArray.Append(Line.CreateBound(new XYZ(0,30,0),new XYZ(0,0,0)));
                //在中间添加洞口
              curveArray.Append(Line.CreateBound(new XYZ(5,5,0),new XYZ(5,15,0) ));
              curveArray.Append(Line.CreateBound(new XYZ(5,15,0),new XYZ(15,5,0)) );
              curveArray.Append(Line.CreateBound(new XYZ(15,5,0),new XYZ(5,5,0) ));
                //创建屋顶
                ModelCurveArray modelCurveArray = new ModelCurveArray();
                FootPrintRoof roof = doc.Create.NewFootPrintRoof(curveArray, level, roofType, out modelCurveArray);
                //设置屋顶坡度
                ModelCurve curve1 = modelCurveArray.get_Item(0);
                ModelCurve curve3 = modelCurveArray.get_Item(2);
                roof.set_DefinesSlope(curve1,true);
                roof.set_SlopeAngle(curve1,0.5);

                roof.set_DefinesSlope(curve3,true);
                roof.set_SlopeAngle(curve3,1.6);



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