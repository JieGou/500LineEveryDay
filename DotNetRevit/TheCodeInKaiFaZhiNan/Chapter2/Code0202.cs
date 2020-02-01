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
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

using View = Autodesk.Revit.DB.View;

namespace TheCodeInKaiFaZhiNan.Chapter2
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class Code0202 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /* revit开发指南 第二章 代码2-2 */

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;


            //using boundingboxIntersectsFilter过滤器得到与该轮廓相交的图元.
            Outline myOutline = new Outline(new XYZ(0, 0, 0), new XYZ(100, 100, 100));

            //create a boundingboxIntersects filter with this Outline
            BoundingBoxIntersectsFilter filter = new BoundingBoxIntersectsFilter(myOutline);

            //apply the filter to the elements in the active document
            //this filter excludes all objects derived from view and objects derived from elementtype
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> elements2 = collector.WherePasses(filter).ToElements();

            //find walls which donot intersect with boundingbox,use an inverted filter
            BoundingBoxIntersectsFilter invertFilter = new BoundingBoxIntersectsFilter(myOutline, true);
            collector = new FilteredElementCollector(doc);
            IList<Element> noIntersectWalls = collector.OfClass(typeof(Wall)).WherePasses(invertFilter).ToElements();

            Transaction ts = new Transaction(doc, "**");

            ts.Start();

            ts.Commit();

            return Result.Succeeded;
        }
    }
}