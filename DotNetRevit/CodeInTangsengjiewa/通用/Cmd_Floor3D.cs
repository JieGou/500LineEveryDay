using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa.BinLibrary.Helpers;
using CodeInTangsengjiewa.通用.UIs;

namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.通用
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_Floor3D : IExternalCommand
    {/// <summary>
    /// 功能未实现!! 具体原因未找到.
    /// </summary>
    /// <param name="commandData"></param>
    /// <param name="message"></param>
    /// <param name="elements"></param>
    /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            var acview = doc.ActiveView;

            var viewfamilytypes = doc.TCollector<ViewFamilyType>();
            var viewplanfamilytype = viewfamilytypes.First(m => m.ViewFamily == ViewFamily.FloorPlan);
            var view3dfamilytype = viewfamilytypes.First(m => m.ViewFamily == ViewFamily.ThreeDimensional);

            var levels = doc.TCollector<Level>();

            FloorSelector fsui = FloorSelector.Instance;
            fsui.FloorBox.ItemsSource = levels;
            fsui.FloorBox.DisplayMemberPath = "Name";
            fsui.FloorBox.SelectedIndex = 0;
            fsui.ShowDialog();

            var targetfloor = fsui.FloorBox.SelectionBoxItem as Level;
            var upperfloor = levels.Where(m => m.Elevation > targetfloor.Elevation)?.OrderBy(m => m.Elevation)
                ?.FirstOrDefault();
            var categories = doc.Settings.Categories; //
            var modelcategories = categories.Cast<Category>().Where(m => m.CategoryType == CategoryType.Model).ToList();

            var filterslist = new List<ElementFilter>();

            foreach (var modelcategory in modelcategories)
            {
                var categoryfilter = new ElementCategoryFilter(modelcategory.Id);
                filterslist.Add(categoryfilter);
            }

            var logicOrFilter = new LogicalOrFilter(filterslist);

            var collector = new FilteredElementCollector(doc);
            var modelelements = collector.WherePasses(logicOrFilter).WhereElementIsNotElementType()
                .Where(m => m.Category.CategoryType == CategoryType.Model);


            var modelelementsids = modelelements.Select(m => m.Id).ToList();

            var temboudingbox = default(BoundingBoxXYZ);

            Transaction temtran = new Transaction(doc, "temtransaction");
            temtran.Start();
            var temgroup = doc.Create.NewGroup(modelelementsids);
            var temview = ViewPlan.Create(doc, viewplanfamilytype.Id, targetfloor.Id);
            temboudingbox = temgroup.get_BoundingBox(temview);
            temtran.RollBack();

            var zMin = targetfloor.Elevation;
            var zMax = upperfloor?.Elevation ?? temboudingbox.Max.Z;

            var oldmin = temboudingbox.Min;
            var oldmax = temboudingbox.Max;

            BoundingBoxXYZ newbox = new BoundingBoxXYZ();
            newbox.Min = new XYZ(oldmin.X, oldmin.Y, zMin);
            newbox.Max = new XYZ(oldmax.X, oldmax.Y, zMax);
            var new3dview = default(View3D);

            doc.Invoke(m =>
            {
                new3dview = View3D.CreateIsometric(doc, view3dfamilytype.Id);
                new3dview.SetSectionBox(newbox);
            }, "楼层三维");

            uidoc.ActiveView = new3dview;

            return Result.Succeeded;
        }
    }
}