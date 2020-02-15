using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using CodeInTangsengjiewa.BinLibrary.Helpers;
using CodeInTangsengjiewa.BinLibrary.RevitHelper;
using CodeInTangsengjiewa.Test.UIs;
using RevitDevelopmentFoudation.CodeInTangsengjiewa.Test.UIs;

namespace CodeInTangsengjiewa.Test
{
    /// <summary>
    /// 在轴线交点处生成柱子
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_CreateColumnAccordingGridIntersection : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = doc.ActiveView;

            //filter target columntypes
            ElementFilter architectureColumnFilter = new ElementCategoryFilter(BuiltInCategory.OST_Columns);
            ElementFilter structureColumnFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            ElementFilter orFilter = new LogicalOrFilter(architectureColumnFilter, structureColumnFilter);
            var collector = new FilteredElementCollector(doc);
            var columntypes = collector.WhereElementIsElementType().WherePasses(orFilter).ToElements();

            ColumnTypesForm typesForm = ColumnTypesForm.Getinstance(columntypes.ToList());
            typesForm.ShowDialog(RevitWindowhelper.GetRevitWindow());

            //get selected familysymbol of combobox in columntypesForm
            var familysymbol = typesForm.symbolCombo.SelectedItem as FamilySymbol;

            //varient for setting bottom and top /* for learners self modifing */
            // var bottomlevel = default(Level);
            // var bottomoffset = default(double);

            // var toplevel = default(Level);
            // var topoffset = default(string);

            var grids = doc.TCollector<Grid>();
            var points = new List<XYZ>();

            foreach (var grid in grids)
            {
                foreach (var grid1 in grids)
                {
                    if (grid.Id == grid1.Id)
                    {
                        continue;
                    }

                    var curve1 = grid.Curve;
                    var curve2 = grid1.Curve;
                    var res = new IntersectionResultArray();
                    var intersecRes = curve1.Intersect(curve2, out res);

                    if (intersecRes != SetComparisonResult.Disjoint)
                    {
                        if (res != null)
                        {
                            points.Add(res.get_Item(0).XYZPoint);
                        }
                    }
                }
            }

            //distance points on same location
            points = points.Where(
                                  (m, i) => points.FindIndex(n => n.IsAlmostEqualTo(m)
                                                            ) == i
                                 ).ToList();

            TransactionGroup tsg = new TransactionGroup(doc);
            tsg.Start("统一创建柱子");

            foreach (var point in points)
            {
                doc.Invoke(m =>
                {
                    if (!familysymbol.IsActive) familysymbol.Activate();
                    var instance = doc.Create.NewFamilyInstance(point, familysymbol, acview.GenLevel,
                                                                StructuralType.NonStructural);
                }, "创建柱子");
            }

            tsg.Assimilate();
            return Result.Succeeded;
        }

        public XYZ Intersect_Cus(Curve c, Curve c1)
        {
            XYZ result = null;
            IntersectionResultArray resultArray = new IntersectionResultArray();

            var comparisonResult = c.Intersect(c1, out resultArray);

            if (comparisonResult != SetComparisonResult.Disjoint)
            {
                if (resultArray != null)
                {
                    result = resultArray.get_Item(0).XYZPoint;
                }
            }

            return result;
        }
    }
}