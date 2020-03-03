using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    /// <summary>
    /// what can i do with revit api now?
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_CreateWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = uidoc.ActiveGraphicalView;

            //CreateWall

            // Curve curve = Line.CreateBound(new XYZ(10, 10, 0), new XYZ(0, 0, 0));
            // // Curve curve2 = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(-10, -10, 0));
            // IList<Curve> curves = new List<Curve>();
            // curves.Add( Line.CreateBound(new XYZ(10, 10, 0), new XYZ(0, 0, 0)));
            // // curves.Add(curve2);

            IList<Curve> curves = new List<Curve>(); //墙的线条

            curves.Add(Line.CreateBound(new XYZ(0, 0, 0), new XYZ(0, 0, 20))); //正面看墙的平面
            curves.Add(Line.CreateBound(new XYZ(0, 0, 20), new XYZ(20, 0, 20)));
            curves.Add(Line.CreateBound(new XYZ(20, 0, 20), new XYZ(20, 0, 0)));
            curves.Add(Line.CreateBound(new XYZ(20, 0, 0), new XYZ(0, 0, 0)));

            // Curve curve = Line.CreateBound(new XYZ(10, 10, 0), new XYZ(0, 0, 0));

            ElementId walltypeId = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls)
                .OfClass(typeof(WallType)).Cast<WallType>().OrderBy(x => x.Name).FirstOrDefault()?.Id;

            // TaskDialog.Show("tips", walltypeId.GetElement(doc).Name);

            ElementId levelId = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels)
                .OfClass(typeof(Level)).Cast<Level>().OrderBy(x => x.Elevation).FirstOrDefault()
                ?.Id;

            // TaskDialog.Show("tips", levelId.GetElement(doc).Name);
            // Transaction ts = new Transaction(doc, "create wall");
            // ts.Start();
            // Wall.Create(doc, curves, walltypeId, levelId, false);
            // ts.Commit();

            // doc.Invoke(m => { Wall.Create(doc, curve, levelId, false); }, "create wall");
            doc.Invoke(m => { Wall.Create(doc, curves, walltypeId, levelId, false); }, "create wall");
            //curves 是墙的轮廓(正面看墙的平面.)
            // profile
            // An array of planar curves that represent the vertical profile of the wall. 

            return Result.Succeeded;
        }
    }
}