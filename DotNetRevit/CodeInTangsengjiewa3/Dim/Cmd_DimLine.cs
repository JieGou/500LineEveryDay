using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa3.BinLibrary.Extensions;
using CodeInTangsengjiewa3.BinLibrary.Helpers;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace CodeInTangsengjiewa3.Dim
{
    /// <summary>
    /// 为什么不能正常工作?????????
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_DimLine : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Application app = uiapp.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View acview = uidoc.ActiveView;

            var pipe = uidoc.Selection.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Pipe))
                           .GetElement(doc) as Pipe;
            var location = pipe.Location as LocationCurve;

            var ref1 = location.Curve.GetEndPointReference(0);
            //为什么用reference??
            var ref2 = location.Curve.GetEndPointReference(1);

            var referenceArray = new ReferenceArray();
            referenceArray.Append(ref1);
            referenceArray.Append(ref2);
         

            var line = pipe.LocationLine();
            MessageBox.Show(line.Length.ToString());

            Transaction ts = new Transaction(doc, "dim");
            ts.Start();
            doc.Create.NewDimension(acview, line, referenceArray);
            //line: 为相邻轴网所取点构造的新的Line，
            //referenceArray : 为引用的集合.
       
            ts.Commit();

            // doc.Invoke(m => { doc.Create.NewDimension(acview, line, referencearray); }, "dim");
            return Result.Succeeded;
        }
    }
}