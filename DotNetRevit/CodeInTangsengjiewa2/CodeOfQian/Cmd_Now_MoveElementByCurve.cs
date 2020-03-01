using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
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
    public class Cmd_Now_MoveElementByCurve : IExternalCommand
    {
        /// <summary>
        /// what can i do with revit api now?
        /// move wall by curve
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            Selection sel = uidoc.Selection;

            doc.Invoke(m =>
                       {
                         
                           Wall wall = sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(x => x is Wall))
                                           .GetElement(doc) as Wall; //运行时,取消选择会报错提示.
                           Line newWallLine =
                               Line.CreateBound(XYZ.Zero, new XYZ(2000d.MmToFeet(), -1000d.MmToFeet(), 0));
                           (wall.Location as LocationCurve).Curve = newWallLine;
                       }
                     , "移动墙通过Curve");

            return Result.Succeeded;
        }

        // void MoveUsingCurve(Wall wall)
        // {
        //     LocationCurve wallLine = wall.Location as LocationCurve;
        //     XYZ p1 = XYZ.Zero;
        //     XYZ p2 = new XYZ(10, 20, 0);
        //     Line newWallLine = Line.CreateBound(p1, p2);
        //
        //     wallLine.Curve = newWallLine;
        // }
    }
}