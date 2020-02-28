using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;


namespace CodeInTangsengjiewa2.CodeInJeme
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_CreateFillledRegionDimensions : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = uidoc.ActiveGraphicalView;

            var dimensionTypes = doc.TCollector<DimensionType>();
            FloorSelector fsui = FloorSelector.Instance;
            fsui.LabelName.Text = "选择标注样式";
            fsui.FloorBox.ItemsSource = dimensionTypes;
            fsui.FloorBox.DisplayMemberPath = "Name";
            fsui.FloorBox.SelectedIndex = 0;

            // fsui.Title = "选择标注样式";

            fsui.ShowDialog();
            // var targetDimensionType = fsui.FloorBox.SelectionBoxItem as DimensionType;
            string targetDimensionTypeName = ((DimensionType) fsui.FloorBox.SelectionBoxItem).Name;
            // TaskDialog.Show("tips", targetDimensionTypeName);


            var filledRegions = FindFilledRegions(doc, view.Id);

            using (var transaction = new Transaction(doc, "filled regions dimensions"))
            {
                transaction.Start();

                foreach (var filledRegion in filledRegions)
                {
                    CreateDimensions(filledRegion, -1 * view.RightDirection, targetDimensionTypeName);

                    CreateDimensions(filledRegion, view.UpDirection, targetDimensionTypeName);
                }

                transaction.Commit();
            }
            return Result.Succeeded;
        }


        private void CreateDimensions(FilledRegion filledRegion, XYZ dimensionDirection, string typeName)
        {
            var document = filledRegion.Document;

            var view = (View) document.GetElement(filledRegion.OwnerViewId);

            var edgesDirection = dimensionDirection.CrossProduct(view.ViewDirection);

            var edges = FindRegionEdges(filledRegion).Where(x => IsEdgeDirectionSatisfied(x, edgesDirection)).ToList();

            if (edges.Count < 2) return;

            // Se hace este ajuste para que la distancia no 
            // depende de la escala. <<<<<< evaluar para 
            // información de acotado y etiquetado!!!

            var shift = UnitUtils.ConvertToInternalUnits(5 * view.Scale, DisplayUnitType.DUT_MILLIMETERS) *
                        edgesDirection;

            var dimensionLine = Line.CreateUnbound(filledRegion.get_BoundingBox(view).Min + shift, dimensionDirection);

            var references = new ReferenceArray();

            foreach (var edge in edges) references.Append(edge.Reference);

            Dimension dim = document.Create.NewDimension(view, dimensionLine, references);

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
                return false;

            return edgeCurve.Direction.CrossProduct(edgeDirection).IsAlmostEqualTo(XYZ.Zero);
        }

        private static IEnumerable<FilledRegion> FindFilledRegions(Document document, ElementId viewId)
        {
            var collector = new FilteredElementCollector(document, viewId);

            return collector
                .OfClass(typeof(FilledRegion))
                .Cast<FilledRegion>();
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