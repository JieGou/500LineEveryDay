using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CodeInTangsengjiewa3.CodeInHuanGS
{
    /// <summary>
    /// Revit 设置模型线的颜色有两种方法:
    /// 方法一: 新建线的样式,设置
    /// 方法二: 替换视图中的图形,只对当前视图有效.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_ModelLineColorOverRide : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = doc.ActiveView;

            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element elem = doc.GetElement(reference);

            //方法二
            OverrideGraphicSettings ogs = view.GetElementOverrides(elem.Id);
            Transaction ts = new Transaction(doc, "替换视图中的图形");
            ts.Start();
            ogs.SetProjectionLineColor(new Color(0, 255, 0));
            view.SetElementOverrides(elem.Id, ogs);
            ts.Commit();
            return Result.Succeeded;
        }
    }
}