using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_CreateWindow : IExternalCommand
    {
        /// <summary>
        /// what can i do with revit api now?
        /// 创建窗
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = uidoc.ActiveGraphicalView;
            var sel = uidoc.Selection;

            var symbol = new FilteredElementCollector(doc).WhereElementIsElementType()
                             .OfCategory(BuiltInCategory.OST_Windows).OfClass(typeof(FamilySymbol))
                             .FirstOrDefault() as FamilySymbol;

            TaskDialog.Show("tips", symbol.Name);

            Reference reference = sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Wall));
            TaskDialog.Show("tips", reference.GetElement(doc).Name);

            XYZ startPoint = ((reference.GetElement(doc).Location as LocationCurve).Curve as Line).GetEndPoint(0);
            XYZ endPoint = ((reference.GetElement(doc).Location as LocationCurve).Curve as Line).GetEndPoint(1);

            XYZ midPoint = (startPoint + endPoint) / 2;

            Line positon = Line.CreateBound(startPoint, midPoint);

            Level level = doc.TCollector<Level>().FirstOrDefault();

            Transaction ts = new Transaction(doc, "创建窗");
            ts.Start();

            if (!symbol.IsActive)
            {
                symbol.Activate();
            }
            doc.Create.NewFamilyInstance(midPoint, symbol, reference.GetElement(doc), StructuralType.NonStructural);
            ts.Commit();

            return Result.Succeeded;
        }
    }
}