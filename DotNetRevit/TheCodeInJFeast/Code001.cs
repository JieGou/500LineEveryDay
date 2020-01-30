using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassTeacherXu.Helpers;
using View = Autodesk.Revit.DB.View;

namespace TheCodeInJFeast
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class Code001 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /* https://www.bilibili.com/video/av60526429?p=4                  
             */

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();

            //1 选择一根风管
            Reference r = sel.PickObject(ObjectType.PointOnElement);
            Element element1 = doc.GetElement(r);

            Duct duct = element1 as Duct;

            if (duct == null)
            {
                return Result.Failed;
            }

            Parameter parameter = duct.get_Parameter(BuiltInParameter.RBS_CURVE_SURFACE_AREA);
            double s = parameter.AsDouble();

            Parameter parameter2 = duct.get_Parameter(BuiltInParameter.RBS_OFFSET_PARAM);

           // TaskDialog.Show("demo1", s.ToString());

            Transaction ts = new Transaction(doc, "**");

            ts.Start();

            parameter2.SetValueString("3000");//修改文档的数据时.必须开启一个事务,确保数据安全.
            //获取数据是安全的,不用开启事务.

            ts.Commit();

            return Result.Succeeded;
        }
    }
}