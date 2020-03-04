#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CreationApp = Autodesk.Revit.Creation.Application;
using RvtOperationCanceledException = Autodesk.Revit.Exceptions.OperationCanceledException;
#endregion

namespace CodeInTangsengjiewa2.CodeInJeme
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Cmd_SelectTouchingBeams : IExternalCommand
    {
        /// <summary>
        /// Size of sphere within which elements 
        /// are considered connected
        /// </summary>
        const double _sphere_radius = 5;

        /// <summary>
        /// We are only interested in element 
        /// of the following category
        /// </summary>
        const BuiltInCategory _bic
            = BuiltInCategory.OST_StructuralFraming;

        /// <summary>
        /// Return a string describing the given element:
        /// .NET type name,
        /// category name,
        /// family and symbol name for a family instance,
        /// element id and element name.
        /// </summary>
        public static string ElementDescription(
            Element e)
        {
            if (null == e)
            {
                return "<null>";
            }

            // For a wall, the element name equals the
            // wall type name, which is equivalent to the
            // family name ...

            FamilyInstance fi = e as FamilyInstance;

            string typeName = e.GetType().Name;

            string categoryName = (null == e.Category)
                                      ? string.Empty
                                      : e.Category.Name + " ";

            string familyName = (null == fi)
                                    ? string.Empty
                                    : fi.Symbol.Family.Name + " ";

            string symbolName = (null == fi
                                 || e.Name.Equals(fi.Symbol.Name))
                                    ? string.Empty
                                    : fi.Symbol.Name + " ";

            return string.Format("{0} {1}{2}{3}<{4} {5}>",
                                 typeName, categoryName, familyName, symbolName,
                                 e.Id.IntegerValue, e.Name);
        }

        /// <summary>
        /// Create and return a solid sphere with
        /// a given radius and centre point.
        /// </summary>
        static public Solid CreateSphereAt(
            Document doc,
            XYZ centre,
            double radius)
        {
            // Use the standard global coordinate system 
            // as a frame, translated to the sphere centre.

            Frame frame = new Frame(centre,
                                    XYZ.BasisX, XYZ.BasisY, XYZ.BasisZ);

            // Create a vertical half-circle loop;
            // this must be in the frame location.

            Arc arc = Arc.Create(
                                 centre - radius * XYZ.BasisZ,
                                 centre + radius * XYZ.BasisZ,
                                 centre + radius * XYZ.BasisX);

            Line line = Line.CreateBound(
                                         arc.GetEndPoint(1),
                                         arc.GetEndPoint(0));

            CurveLoop halfCircle = new CurveLoop();
            halfCircle.Append(arc);
            halfCircle.Append(line);

            List<CurveLoop> loops = new List<CurveLoop>(1);
            loops.Add(halfCircle);

            return GeometryCreationUtilities
                .CreateRevolvedGeometry(
                                        frame, loops, 0, 2 * Math.PI);
        }

        /// <summary>
        /// Determine all neighbouring elements connected
        /// to the current element 'e', skipping all 
        /// previously visited ones.
        /// </summary>
        void AddElementsIntersectingSphereAt(
            List<ElementId> neighbours,
            XYZ p,
            List<ElementId> visited,
            Document doc)
        {
            Solid sphere = CreateSphereAt(
                                          doc, p, _sphere_radius);

            ElementIntersectsSolidFilter intersectSphere
                = new ElementIntersectsSolidFilter(sphere);

            FilteredElementCollector collector
                = new FilteredElementCollector(doc)
                    .WhereElementIsCurveDriven() // we work with the location curve
                    .OfCategory(_bic)
                    .Excluding(visited.Union<ElementId>(
                                                        neighbours).ToList<ElementId>())
                    .WherePasses(intersectSphere);

            //.Excluding( neighbours.ConvertAll<ElementId>( x => x.Id ) )

            // The argument to Expluding must not be empty, 
            // or an exception is thrown; therefore, union
            // the visited with the neighbours, since the 
            // visited list is never empty.
            //.Excluding( neighbours )

            //foreach( Element e in collector )
            //{
            //  if( !visited.Contains( e.Id )
            //    && !neighbours.ConvertAll<ElementId>( 
            //      x => x.Id ).Contains( e.Id ) )
            //  {
            //    neighbours.Add( e );
            //  }
            //}

            neighbours.AddRange(collector.ToElementIds());
        }

        /// <summary>
        /// Determine all neighbouring elements close to 
        /// the two ends of the current element 'e', 
        /// skipping all previously visited ones.
        /// </summary>
        void AddConnectedElements(
            List<ElementId> neighbours,
            Element e,
            List<ElementId> visited)
        {
            Location loc = e.Location;

            Debug.Print(string.Format(
                                      "current element {0} has location {1}",
                                      ElementDescription(e),
                                      null == loc ? "<null>" : loc.GetType().Name));

            LocationCurve lc = loc as LocationCurve;

            if (null != lc)
            {
                Document doc = e.Document;

                Curve c = lc.Curve;

                XYZ p = c.GetEndPoint(0);
                XYZ q = c.GetEndPoint(1);

                AddElementsIntersectingSphereAt(
                                                neighbours, p, visited, doc);

                AddElementsIntersectingSphereAt(
                                                neighbours, q, visited, doc);
            }
        }

        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            CreationApp creapp = app.Create;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            Reference r = null;

            try
            {
                r = sel.PickObject(
                                   ObjectType.Element,
                                   "Please select a beam");
            }
            catch (RvtOperationCanceledException)
            {
                return Result.Cancelled;
            }

            // Starting element

            Element start = doc.GetElement(r);

            // The current elements whose neighbours 
            // we are seeking

            List<ElementId> current = new List<ElementId>();

            current.Add(start.Id);

            // List of elements already visited

            List<ElementId> visited = new List<ElementId>();

            // Continue as long as new connected 
            // elements are found

            List<ElementId> neighbours = new List<ElementId>();

            while (0 < current.Count)
            {
                // Remember where we have been, add this to 
                // the result so far, and do not revisit these

                //visited.AddRange( 
                //  current.ConvertAll<ElementId>( 
                //    e => e.Id ) );

                visited.AddRange(current);

                // We found no new neighbours yet

                neighbours.Clear();

                // Search current elements for new connected 
                // elements not already visited

                foreach (ElementId id in current)
                {
                    Element e = doc.GetElement(id);

                    AddConnectedElements(
                                         neighbours, e, visited);
                }

                // Done with the current elements, and the
                // newly found become the next current ones

                current.Clear();
                current.AddRange(neighbours);
            }

            ICollection<ElementId> ids = new List<ElementId>();
            foreach (ElementId id in visited)
            {
                ids.Add(id);
            }
            sel.SetElementIds(ids);
            return Result.Succeeded;
        }

        #region Sample Code from Revit 2012 Help File
        // public ICollection<ElementId> FindWallJoinsAtEndUsingProximity(Wall wall, int end)
        // {
        //     // Get properties of wall at the end point 
        //
        //     LocationCurve wallCurve = wall.Location as LocationCurve;
        //
        //     XYZ endPoint = wallCurve.Curve.GetEndPoint(end);
        //
        //     double height = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble();
        //
        //     double elevation = ((wall.LevelId) as Level).Elevation;
        //
        //
        //     // Build cylinder centered at wall end point, extending 3' in diameter 
        //
        //     CurveLoop cylinderLoop = new CurveLoop();
        //
        //     XYZ arcCenter = new XYZ(endPoint.X, endPoint.Y, elevation);
        //
        //     Application application = wall.Document.Application;
        //
        //     Arc firstArc = Arc.Create(arcCenter, 1.5, 0, Math.PI, XYZ.BasisX, XYZ.BasisY);
        //
        //     Arc secondArc = Arc.Create(arcCenter, 1.5, Math.PI, 2 * Math.PI, XYZ.BasisX, XYZ.BasisY);
        //
        //     cylinderLoop.Append(firstArc);
        //
        //     cylinderLoop.Append(secondArc);
        //
        //     List<CurveLoop> singleLoop = new List<CurveLoop>();
        //
        //     singleLoop.Add(cylinderLoop);
        //
        //     Solid proximityCylinder = GeometryCreationUtilities.CreateExtrusionGeometry(singleLoop, XYZ.BasisZ, height);
        //
        //     // Filter walls 
        //
        //     FilteredElementCollector proximityCollector = new FilteredElementCollector(wall.Document);
        //
        //     proximityCollector.OfClass(typeof(Wall));
        //
        //     // Exclude the wall itself 
        //
        //     List<ElementId> toExclude = new List<ElementId>();
        //
        //     toExclude.Add(wall.Id);
        //
        //     proximityCollector.Excluding(toExclude);
        //
        //     // Filter only elements intersecting our target cylinder 
        //
        //     proximityCollector.WherePasses(new ElementIntersectsSolidFilter(proximityCylinder));
        //
        //     // Return matches 
        //
        //     return proximityCollector.ToElementIds();
        // }
        #endregion // Sample Code from Revit 2012 Help File
    }

    // //后面这段代码不知道干嘛的.决定先不管.
    // #region Display sphere using AVF
    // [Transaction(TransactionMode.Manual)]
    // public class CommandDisplaySphere : IExternalCommand
    // {
    //     #region AVF Functionality
    //     void CreateAvfDisplayStyle(
    //         Document doc,
    //         View view)
    //     {
    //         using (Transaction t = new Transaction(doc))
    //         {
    //             t.Start("Create AVF Style");
    //             AnalysisDisplayColoredSurfaceSettings
    //                 coloredSurfaceSettings = new
    //                     AnalysisDisplayColoredSurfaceSettings();
    //
    //             coloredSurfaceSettings.ShowGridLines = false;
    //
    //             AnalysisDisplayColorSettings colorSettings
    //                 = new AnalysisDisplayColorSettings();
    //
    //             AnalysisDisplayLegendSettings legendSettings
    //                 = new AnalysisDisplayLegendSettings();
    //
    //             legendSettings.ShowLegend = false;
    //
    //             AnalysisDisplayStyle analysisDisplayStyle
    //                 = AnalysisDisplayStyle
    //                     .CreateAnalysisDisplayStyle(doc,
    //                                                 "Paint Solid", coloredSurfaceSettings,
    //                                                 colorSettings, legendSettings);
    //
    //             view.AnalysisDisplayStyleId
    //                 = analysisDisplayStyle.Id;
    //
    //             t.Commit();
    //         }
    //     }
    //
    //     static int _schemaId = -1;
    //
    //     void PaintSolid(
    //         Document doc,
    //         Solid s,
    //         double value)
    //     {
    //         Application app = doc.Application;
    //
    //         View view = doc.ActiveView;
    //
    //         if (view.AnalysisDisplayStyleId
    //             == ElementId.InvalidElementId)
    //         {
    //             CreateAvfDisplayStyle(doc, view);
    //         }
    //
    //         SpatialFieldManager sfm
    //             = SpatialFieldManager.GetSpatialFieldManager(
    //                                                          view);
    //
    //         if (null == sfm)
    //         {
    //             sfm = SpatialFieldManager
    //                 .CreateSpatialFieldManager(view, 1);
    //         }
    //
    //         if (_schemaId != -1)
    //         {
    //             IList<int> results
    //                 = sfm.GetRegisteredResults();
    //
    //             if (!results.Contains(_schemaId))
    //             {
    //                 _schemaId = -1;
    //             }
    //         }
    //
    //         if (_schemaId == -1)
    //         {
    //             AnalysisResultSchema resultSchema
    //                 = new AnalysisResultSchema(
    //                                            "PaintedSolids", "Description");
    //
    //             _schemaId = sfm.RegisterResult(resultSchema);
    //         }
    //
    //         FaceArray faces = s.Faces;
    //         Transform trf = Transform.Identity;
    //
    //         foreach (Face face in faces)
    //         {
    //             int idx = sfm.AddSpatialFieldPrimitive(
    //                                                    face, trf);
    //
    //             IList<UV> uvPts = new List<UV>(1);
    //             uvPts.Add(face.GetBoundingBox().Min);
    //
    //             FieldDomainPointsByUV pnts
    //                 = new FieldDomainPointsByUV(uvPts);
    //
    //             List<double> doubleList = new List<double>(1);
    //             doubleList.Add(value);
    //
    //             IList<ValueAtPoint> valList
    //                 = new List<ValueAtPoint>(1);
    //
    //             valList.Add(new ValueAtPoint(doubleList));
    //
    //             FieldValues vals = new FieldValues(valList);
    //
    //             sfm.UpdateSpatialFieldPrimitive(
    //                                             idx, pnts, vals, _schemaId);
    //         }
    //     }
    //     #endregion // AVF Functionality
    //
    //     public Result Execute(
    //         ExternalCommandData commandData,
    //         ref string message,
    //         ElementSet elements)
    //     {
    //         UIApplication uiapp = commandData.Application;
    //         UIDocument uidoc = uiapp.ActiveUIDocument;
    //         Application app = uiapp.Application;
    //         CreationApp creapp = app.Create;
    //         Document doc = uidoc.Document;
    //
    //         Solid s1 = Cmd_SelectTouchingBeams.CreateSphereAt(
    //                                           doc, XYZ.Zero, 1.0);
    //
    //         Solid s2 = Cmd_SelectTouchingBeams.CreateSphereAt(
    //                                           doc, new XYZ(44.051020645,
    //                                                        80.747278319, 9.842519685), 1.0);
    //
    //         Solid s3 = Cmd_SelectTouchingBeams.CreateSphereAt(
    //                                           doc, 5 * XYZ.BasisX, 3.0);
    //
    //         Solid s4 = Cmd_SelectTouchingBeams.CreateSphereAt(
    //                                           doc, 10 * XYZ.BasisY, 5.0);
    //
    //         PaintSolid(doc, s1, 1.0);
    //         PaintSolid(doc, s2, 2.0);
    //         PaintSolid(doc, s3, 3.0);
    //         PaintSolid(doc, s4, 4.0);
    //
    //         return Result.Succeeded;
    //     }
    //
    //     #region Geometry 2012 from DevDays 2010 Online with Revit 2012 API news
    //     // http://thebuildingcoder.typepad.com/blog/2011/04/devdays-2010-online-with-revit-2012-api-news.html#3
    //     // file:///C:/a/doc/revit/blog/694_avf_hilite_rooms.htm
    //
    //     [Transaction(TransactionMode.Manual)]
    //     public class RestoreViewCommand : IExternalCommand
    //     {
    //         public Result Execute(
    //             ExternalCommandData commandData,
    //             ref string message,
    //             ElementSet elements)
    //         {
    //             Document doc = commandData.Application.ActiveUIDocument.Document;
    //             Transaction t = new Transaction(doc, "Restore View");
    //             t.Start();
    //             View view = doc.ActiveView;
    //
    //             SpatialFieldManager sfm = SpatialFieldManager.GetSpatialFieldManager(view);
    //             if (sfm != null) sfm.Clear();
    //
    //             //Categories categories = doc.Settings.Categories;
    //
    //             //HighlightIntersectionsCommand.SetCategoryVisible( categories, BuiltInCategory.OST_Walls, view );
    //             //HighlightIntersectionsCommand.SetCategoryVisible( categories, BuiltInCategory.OST_Columns, view );
    //             //HighlightIntersectionsCommand.SetCategoryVisible( categories, BuiltInCategory.OST_StructuralColumns, view );
    //
    //             t.Commit();
    //
    //             return Result.Succeeded;
    //         }
    //         //}
    //
    //         private int schemaId = -1;
    //
    //         private void PaintSolid(Document doc, Solid s, double value)
    //         {
    //             Application app = doc.Application;
    //
    //             View view = doc.ActiveView;
    //
    //             if (view.AnalysisDisplayStyleId == ElementId.InvalidElementId)
    //             {
    //                 CreateAVFDisplayStyle(doc, view);
    //             }
    //
    //             SpatialFieldManager sfm = SpatialFieldManager.GetSpatialFieldManager(view);
    //             if (sfm == null) sfm = SpatialFieldManager.CreateSpatialFieldManager(view, 1);
    //
    //             if (schemaId != -1)
    //             {
    //                 IList<int> results = sfm.GetRegisteredResults();
    //
    //                 if (!results.Contains(schemaId))
    //                 {
    //                     schemaId = -1;
    //                 }
    //             }
    //
    //             if (schemaId == -1)
    //             {
    //                 AnalysisResultSchema resultSchema1 = new AnalysisResultSchema("PaintedSolid", "Description");
    //                 schemaId = sfm.RegisterResult(resultSchema1);
    //             }
    //
    //             FaceArray faces = s.Faces;
    //             Transform trf = Transform.Identity;
    //
    //             foreach (Face face in faces)
    //             {
    //                 int idx = sfm.AddSpatialFieldPrimitive(face, trf);
    //
    //                 IList<UV> uvPts = new List<UV>();
    //                 List<double> doubleList = new List<double>();
    //                 IList<ValueAtPoint> valList = new List<ValueAtPoint>();
    //                 BoundingBoxUV bb = face.GetBoundingBox();
    //                 uvPts.Add(bb.Min);
    //                 doubleList.Add(value);
    //                 valList.Add(new ValueAtPoint(doubleList));
    //                 FieldDomainPointsByUV pnts = new FieldDomainPointsByUV(uvPts);
    //                 FieldValues vals = new FieldValues(valList);
    //
    //                 sfm.UpdateSpatialFieldPrimitive(idx, pnts, vals, schemaId);
    //             }
    //         }
    //
    //         private void CreateAVFDisplayStyle(Document doc, View view)
    //         {
    //             Transaction t = new Transaction(doc, "Create AVF style");
    //             t.Start();
    //             AnalysisDisplayColoredSurfaceSettings coloredSurfaceSettings =
    //                 new AnalysisDisplayColoredSurfaceSettings();
    //             coloredSurfaceSettings.ShowGridLines = true;
    //             AnalysisDisplayColorSettings colorSettings = new AnalysisDisplayColorSettings();
    //             AnalysisDisplayLegendSettings legendSettings = new AnalysisDisplayLegendSettings();
    //             legendSettings.ShowLegend = false;
    //             AnalysisDisplayStyle analysisDisplayStyle =
    //                 AnalysisDisplayStyle.CreateAnalysisDisplayStyle(doc, "Paint Solid", coloredSurfaceSettings,
    //                                                                 colorSettings, legendSettings);
    //
    //             view.AnalysisDisplayStyleId = analysisDisplayStyle.Id;
    //             t.Commit();
    //         }
    //     }
    //     #endregion // Geometry 2012 from DevDays 2010 Online with Revit 2012 API news
    // }
    // #endregion // Display sphere using AVF
}