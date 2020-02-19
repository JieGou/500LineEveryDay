using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Transaction = Autodesk.Revit.DB.Transaction;
using TransactionStatus = Autodesk.Revit.DB.TransactionStatus;

namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.通用
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_HideElementInfamilyDoc : IExternalCommand
    {/// <summary>
    ///  影藏组文档汇中的元素
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

            var ele = sel.PickObject(ObjectType.Element);

            Transaction ts = new Transaction(doc, "****");

            try
            {
                ts.Start();
                acview.HideElements(new Collection<ElementId>() {ele.ElementId});
                ts.Commit();
            }
            catch (Exception e)
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