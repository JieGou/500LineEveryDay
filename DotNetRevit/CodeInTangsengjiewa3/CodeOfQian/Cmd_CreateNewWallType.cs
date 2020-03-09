using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CodeInTangsengjiewa3.CodeOfQian
{
    /// <summary>
    ///  ■ WallType类型的Duplicate()方法创建一个墙类型,
    ///  ■ 从FamilySymbol.Duplicate() 方法创建一个窗户类型。
    /// code from : https://blog.csdn.net/joexiongjin/article/details/6191299/
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_CreateNewWallType : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;

            //get an exising dimension type.
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(DimensionType));
            DimensionType dimType = null;
            foreach (Element elem in collector)
            {
                if (elem.Name == "Linear Dimension Style")
                {
                    dimType = elem as DimensionType;
                    break;
                }
            }
            DimensionType newType = dimType.Duplicate("NewType") as DimensionType;
            if (newType != null)
            {
                Transaction trans = new Transaction(doc, "ExComm");
                trans.Start();
                newType.get_Parameter(BuiltInParameter.LINE_PEN).Set(2);
                //you can change more here.
                doc.Regenerate();
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}