using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;

namespace CodeInTangsengjiewa2.通用
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_Floor3D : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            var acview = doc.ActiveView;

            var viewfamilytypes = doc.TCollector<ViewFamilyType>();
            var viewplanfamilytype = viewfamilytypes.First(m => m.ViewFamily == ViewFamily.FloorPlan);
            var view3Dfamilytype = viewfamilytypes.First(m => m.ViewFamily == ViewFamily.ThreeDimensional);

            var levels = doc.TCollector<Level>();

            FloorSelector fsui = FloorSelector.Instance;
            fsui.FloorBox.ItemsSource = levels;
            fsui.FloorBox.DisplayMemberPath = "Name";
            fsui.FloorBox.SelectedIndex = 0;
            fsui.ShowDialog();

            var targetfloor = fsui.FloorBox.SelectionBoxItem as Level;
            var upperfloor = levels.Where(m => m.Elevation > targetfloor.Elevation)?.OrderBy(m => m.Elevation)?.First();

            var catrgories = doc.Settings.Categories;
            var modelcategories = catrgories.Cast<Category>().Where(m => m.CategoryType == CategoryType.Model).ToList();

            var filterslist = new List<ElementFilter>();
            foreach (var modelcategory in modelcategories)
            {
                var categoryfilter = new ElementCategoryFilter(modelcategory.Id);
                filterslist.Add(categoryfilter);
            }
            var logicalOrFilter = new LogicalOrFilter(filterslist);
            var collector = new FilteredElementCollector(doc);
            var modelelemetns = collector.WherePasses(logicalOrFilter).WhereElementIsNotElementType()
                .Where(m => m.Category.CategoryType == CategoryType.Model);

            var modelelementsids = modelelemetns.Select(m => m.Id).ToList();
            var temboundingbox = default(BoundingBoxXYZ);

            Transaction temtran = new Transaction(doc, "temtransaction");
            temtran.Start();
            var temgroup = doc.Create.NewGroup(modelelementsids);
            var temview = ViewPlan.Create(doc, viewplanfamilytype.Id, targetfloor.Id);
            temboundingbox = temgroup.get_BoundingBox(temview);
            temtran.RollBack();

            var zMin = targetfloor.Elevation;
            var zMax = upperfloor?.Elevation ?? temboundingbox.Max.Z;

            var oldmin = temboundingbox.Min;
            var oldmax = temboundingbox.Max;

            BoundingBoxXYZ newbox = new BoundingBoxXYZ();
            newbox.Min = new XYZ(oldmin.X, oldmin.Y, zMin);
            newbox.Max = new XYZ(oldmax.X, oldmax.Y, zMax);
            var new3dview = default(View3D);
            doc.Invoke(m =>
            {
                new3dview = View3D.CreateIsometric(doc, view3Dfamilytype.Id);
                new3dview.SetSectionBox(newbox);
            }, "楼层三维");

            uidoc.ActiveView = new3dview;

            return Result.Succeeded;
        }
    }
}