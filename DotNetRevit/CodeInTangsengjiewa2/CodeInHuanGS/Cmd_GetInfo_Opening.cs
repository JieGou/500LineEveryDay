using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace CodeInTangsengjiewa2.CodeInHuanGS
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_GetInfo_Opening : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            Opening ele =
                sel.PickObject(ObjectType.Element,
                               doc.GetSelectionFilter(m => m is Opening))
                    .GetElement(doc) as Opening;

            if (ele.Category.Id.IntegerValue == (int) BuiltInCategory.OST_ShaftOpening)
            {
                TaskDialog.Show("tips", "竖井洞口,洞口主体为null");
                return Result.Cancelled;
            }
            else
            {
                GetInfoOpening(ele);
                return Result.Succeeded;
            }
        }

        private void GetInfoOpening(Opening opening)
        {
            string message = "Openg: \n";
            //get the host element of this opening
            message += "\n The id of the opening's host element is : " + opening.Host.Id.IntegerValue;

            //get the information whether the opening has a rect boundary
            //if the opening has a rect bounndary, we can get the geometry information from BoundaryRect property.
            //Otherwise we should get the geometry information  from Boundary Curves property
            if (opening.IsRectBoundary)
            {
                message += "\n The opening has a rectangle boundary.";
                //arrary contains two XYZ objects; the max and min coords of boundary
                IList<XYZ> boundaryRect = opening.BoundaryRect;

                //get the coordinate value of the min coordinate point
                XYZ point = boundaryRect[0];
                message += "\n Min coordinate point: " + XyzToString(point);

                //get the coordinate value of the Max coordinate point
                point = boundaryRect[1];
                message += "\n Max coordinate point: " + XyzToString(point);
            }

            else
            {
                message += "\n The opening does not have a rectangle boundary.";
                //get curve number
                int curves = opening.BoundaryCurves.Size;
                message += "\nNumber of curves is : " + curves;
                for (int i = 0; i < curves; i++)
                {
                    Curve curve = opening.BoundaryCurves.get_Item(i);
                    //get curve start point
                    message += "\n Curve start point: " + XyzToString(curve.GetEndPoint(0));
                    //get curve end point
                    message += "\n Curve end point: " + XyzToString(curve.GetEndPoint(1));
                }
            }

            TaskDialog.Show("info of opening", message);
        }

        //output the point's coordinate
        string XyzToString(XYZ point)
        {
            return "(" + point.X.ToString("F") + "," + point.Y.ToString("F") + "," + point.Z.ToString("F") + ")";
        }
    }
}