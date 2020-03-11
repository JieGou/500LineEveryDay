using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using CodeInTangsengjiewa3.BinLibrary.Extensions;
using CodeInTangsengjiewa3.BinLibrary.Helpers;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace CodeInTangsengjiewa3.建筑
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CutFloorWithLine : IExternalCommand
    {
        private Floor floor = null;
        ICollection<ElementId> ids_add = new List<ElementId>();
        private Application App = null;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            App = app;
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = uidoc.ActiveView;
            //
            // app.DocumentChanged += OnDocumentChanged;
        }

        private void OnIdling(object sender, IdlingEventArgs e)
        {
            var docsInApp = App.Documents.Cast<Document>();
            var doc = docsInApp.FirstOrDefault();
            var uidoc = new UIDocument(doc);
            var uiapp = uidoc.Application;
            try
            {
                var ActiveDoc = docsInApp.FirstOrDefault();
                if (ids_add.Count != 1)
                {
                    return;
                }
                var modelLine = ids_add.First().GetElement(ActiveDoc) as ModelLine;
                var line = modelLine.GeometryCurve as Line;
                var lineDir = line.Direction;
                var startPoint = line.StartPoint();
                var endPoint = line.EndPoint();

                var upDir = XYZ.BasisZ;
                var leftNorm = upDir.CrossProduct(lineDir).Normalize();
                var rightNorm = upDir.CrossProduct(-lineDir).Normalize();

                var leftSpacePlane = default(Plane);
                var rightSpacePlane = default(Plane);

                var geoEle = floor.get_Geometry(new Options()
                {
                    ComputeReferences = true,
                    DetailLevel = ViewDetailLevel.Fine
                });
                var solid = geoEle.GetSolids().FirstOrDefault();
                var newSolid1 = BooleanOperationsUtils.CutWithHalfSpace(solid, leftSpacePlane);
                var newSolid2 = BooleanOperationsUtils.CutWithHalfSpace(solid, rightSpacePlane);

                var upface1 = newSolid1.GetFacesOfGeometryObject()
                    .Where(m => m.ComputeNormal(new UV()).IsSameDirection(XYZ.BasisZ)).FirstOrDefault();
                var upface2 = newSolid2.GetFacesOfGeometryObject()
                    .Where(m => m.ComputeNormal(new UV()).IsSameDirection(XYZ.BasisZ)).FirstOrDefault();

                var curveLoop1 = upface1.GetEdgesAsCurveLoops().FirstOrDefault();
                var curveArray = curveLoop1.ToCurveArray();
                var curveLoop2 = upface2.GetEdgesAsCurveLoops().FirstOrDefault();
                var curveArray2 = curveLoop2.ToCurveArray();

                ActiveDoc.Invoke(m =>
                {
                    var newFloor1 = ActiveDoc.Create.NewFloor(curveArray, floor.FloorType,
                                                              floor.LevelId.GetElement(ActiveDoc) as Level, false);
                    var newFloor2 = ActiveDoc.Create.NewFloor(curveArray, floor.FloorType,
                                                              floor.LevelId.GetElement(ActiveDoc) as Level, false);
                    doc.Delete(floor.Id);
                }, "new floor");
                if (this != null)
                {
                    uiapp.Idling -= OnIdling;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                uiapp.Idling -= OnIdling;
            }
        }

        private void OnDocumentChanged(Object sender, DocumentChangedEventArgs e)
        {
            var app = sender as Application;
            try
            {
                var docsInApp = App.Documents.Cast<Document>();
                var ActiveDoc = e.GetDocument();
                ids_add = e.GetAddedElementIds();
                if (ids_add.Count != 1)
                {
                    return;
                }
                var modelLine = ids_add.First().GetElement(ActiveDoc) as ModelLine;
                var line = modelLine.GeometryCurve as Line;
                var lineDir = line.Direction;
                var startPoint = line.StartPoint();
                var endPoint = line.EndPoint();
                var upDir = XYZ.BasisZ;
                var leftNorm = upDir.CrossProduct(lineDir).Normalize();
                var rightNorm = upDir.CrossProduct(-lineDir).Normalize();

                var leftSpacePlane = default(Plane);
                var rightSpacePlane = default(Plane);
                var slapshapeEditor = floor.SlabShapeEditor;
                //剪切楼板
                var geoEle = floor.get_Geometry(new Options()
                {
                    ComputeReferences = true, DetailLevel = ViewDetailLevel.Fine
                });
                var solid = geoEle.GetSolids().FirstOrDefault();

                var newSolid1 = BooleanOperationsUtils.CutWithHalfSpace(solid, leftSpacePlane);
                var newSolid2 = BooleanOperationsUtils.CutWithHalfSpace(solid, rightSpacePlane);

                var upFace1 = newSolid1.GetFacesOfGeometryObject()
                    .FirstOrDefault(m => m.ComputeNormal(new UV()) == XYZ.BasisZ);

                var curveLoop = upFace1.GetEdgesAsCurveLoops().FirstOrDefault();
                var curveArray = curveLoop.ToCurveArray();
                ActiveDoc.Invoke(m =>
                {
                    var newFloor = ActiveDoc.Create.NewFloor(curveArray, floor.FloorType,
                                                             floor.LevelId.GetElement(ActiveDoc) as Level, false);
                }, "new floor");
                if (this != null)
                {
                    App.DocumentChanged -= OnDocumentChanged;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                App.DocumentChanged -= OnDocumentChanged;
            }
        }
    }
}