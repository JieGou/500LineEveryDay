using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa3.BinLibrary.Helpers;
using CodeInTangsengjiewa3.通用.UIs;

namespace CodeInTangsengjiewa3.CodeInJeme
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CreateFillledRegionDimensions : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = uidoc.ActiveView;

            var dimensionTypes = doc.TCollector<DimensionType>();
            FloorSelector fusi = FloorSelector.Instance;
            fusi.LabelName.Text = "选择标注样式";
            fusi.FloorBox.ItemsSource = dimensionTypes;
            fusi.FloorBox.DisplayMemberPath = "Name";
            fusi.FloorBox.SelectedIndex = 0;
            fusi.ShowDialog();
            string targetDimensionTypeName = ((DimensionType) fusi.FloorBox.SelectionBoxItem).Name;
            var filledRegions = FindFilledRegions(doc, view.Id);
            using (Transaction ts = new Transaction(doc, "filled regions dimensions"))
            {
                ts.Start();
                foreach (FilledRegion filledRegion in filledRegions)
                {
                    CreateDimension(filledRegion, -1 * view.RightDirection, targetDimensionTypeName);
                    CreateDimension(filledRegion, view.UpDirection, targetDimensionTypeName);
                }
                ts.Commit();

            }

            return Result.Succeeded;
        }

        private void CreateDimension(FilledRegion filledRegion, XYZ dimensionDirection, string typeName)
        {
            var document = filledRegion.Document;
            var view = (View) document.GetElement(filledRegion.OwnerViewId);
            var edgesDirection = dimensionDirection.CrossProduct(view.ViewDirection);
            var edges = FindRegionEdges(filledRegion).Where(x => IsEdgeDirectionSatisfied(x, edgesDirection)).ToList();
            if (edges.Count < 2) return;
            var shift = UnitUtils.ConvertToInternalUnits(5 * view.Scale, DisplayUnitType.DUT_MILLIMETERS) *
                        edgesDirection; // ???????????????????????
            var dimensionLine = Line.CreateUnbound(filledRegion.get_BoundingBox(view).Min + shift, dimensionDirection);
            var reference = new ReferenceArray();

            foreach (Edge edge in edges)
            {
                reference.Append(edge.Reference);
            }
            Dimension dim = document.Create.NewDimension(view, dimensionLine, reference);
            ElementId dr_id = DimensionTypeId(document, typeName);
            if (dr_id != null)
            {
                dim.ChangeTypeId(dr_id);
            }
        }

        private static bool IsEdgeDirectionSatisfied(Edge edge, XYZ edgeDirection)
        {
            var edgeCurve = edge.AsCurve() as Line;
            if (edgeCurve == null)
            {
                return false;
            }
            return edgeCurve.Direction.CrossProduct(edgeDirection).IsAlmostEqualTo(XYZ.Zero);
        }

        private static IEnumerable<FilledRegion> FindFilledRegions(Document doc, ElementId viewid)
        {
            var collector = new FilteredElementCollector(doc, viewid);
            return collector.OfClass(typeof(FilledRegion)).Cast<FilledRegion>();
        }

        private static IEnumerable<Edge> FindRegionEdges(FilledRegion filledRegion)
        {
            var view = (View) filledRegion.Document.GetElement(filledRegion.OwnerViewId);
            var options = new Options
            {
                View = view,
                ComputeReferences = true
            };
            return filledRegion
                .get_Geometry(options)
                .OfType<Solid>()
                .SelectMany(x => x.Edges.Cast<Edge>());
        }

        private static ElementId DimensionTypeId(Document doc, string typeName)
        {
            FilteredElementCollector mt_coll = new FilteredElementCollector(doc).OfClass(typeof(DimensionType))
                .WhereElementIsElementType();
            DimensionType dimType = null;
            foreach (Element type in mt_coll)
            {
                if (type is DimensionType)
                {
                    if (type.Name == typeName)
                    {
                        dimType = type as DimensionType;
                        break;
                    }
                }
            }
            return dimType.Id;
        }
    }
}