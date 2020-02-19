//
//
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB;
// using CodeInTangsengjiewa.BinLibrary.Extensions;
// using CodeInTangsengjiewa.BinLibrary.Helpers;
// using CodeInTangsengjiewa.建筑;
// using Autodesk.Revit.DB.Events;
// using Autodesk.Revit.UI;
// using Autodesk.Revit.UI.Selection;
// using Application = Autodesk.Revit.ApplicationServices.Application;
//
// namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.建筑
// {
//     [Transaction(TransactionMode.Manual)]
//     class Cmd_CutFloorWithLine : IExternalCommand
//     {
//         private FloorType floor = null;
//         ICollection<ElementId> ids_add = new List<ElementId>();
//         private Application App = null;
//
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             var uiapp = commandData.Application;
//             var app = uiapp.Application;
//             App = app;
//             var uidoc = uiapp.ActiveUIDocument;
//
//             var doc = uidoc.Document;
//             var sel = uidoc.Selection;
//
//             var acview = doc.ActiveView;
//             app.DocumentChanged += OnDocumentChanged;
//             uiapp.Idling += OnIdling;
//
//             floor =
//                 sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Floor)).GetElement(doc) as Floor;
//
// #if Revit2020
//             CommandHandlerService.invokeCommandHandler("ID_OBJECTS_PROJECT_CURVE");
//
// #endif
// #if Revit2016
//               //调用postablecommand 
//               var commandid = RevitCommandId.LookupPostableCommandId(PostableCommand.ModelLine);
//               uiapp.PostCommand(commandid);
//
// #endif
//             return Result.Succeeded;
//         }
//
//         private void OnIdling(object sender, IdlingEventArgs e)
//         {
//             var docsInApp = App.Documents.Cast<Document>();
//             var doc = docsInApp.FirstOrDefault();
//             var uidoc = new UIDocument(doc);
//             var uiapp = uidoc.Application;
//
//             try
//             {
//                 var ActiveDoc = docsInApp.FirstOrDefault();
//
//                 if (ids_add.Count != 1)
//                 {
//                     return;
//                 }
//
//                 var modelline = ids_add.First().GetElement(ActiveDoc) as ModelLine;
//                 var line = modelline.GeometryCurve as Line;
//
//                 var linedir = line.Direction;
//                 var startpo = line.StartPoint();
//                 var endpo = line.EndPoint();
//
//                 var updir = XYZ.BasisZ;
//
//                 var leftNorm = updir.CrossProduct(linedir).Normalize();
//                 var rightNorm = updir.CrossProduct(-linedir).Normalize();
//                 var leftspacePlane = default(Plane);
//                 var rightspacePlane = default(Plane);
//
// #if Revit2016
//                 leftspacePlane = new Plane(leftNorm,starpo);
//                 rightspacePlane = new Plane(rigthNorm,startpo)
// #endif
// #if Revit2020
//                 leftspacePlane = Plane.CreateByNormalAndOrigin(leftNorm, startpo);
//                 rightspacePlane = Plane.CreateByNormalAndOrigin(rightNorm, startpo);
// #endif
//
//                 var geoele = floor.get_Geometry(new Options()
//                 {
//                     ComputeReferences = true, DetailLevel = ViewDetailLevel.Fine
//                 });
//
//                 var solid = geoele.Getsolids().FirstOrDefault();
//
//                 var newsolid1 = BooleanOperationsUtils.CutWithHalfSpace(solid, leftspacePlane);
//                 var newsolid2 = BooleanOperationsUtils.CutWithHalfSpace(solid, rightspacePlane);
//
//                 var upface1 = newsolid1.GetFacesOfGeometryObject()
//                     .Where(m => m.ComputeNormal(new UV()).IsSameDirection(XYZ.BasisZ)).FirstOrDefault();
//
//                 var upface2 = newsolid2.GetFacesOfGeometryObject()
//                     .Where(m => m.ComputeNormal(new UV()).IsSameDirection(XYZ.BasisZ)).FirstOrDefault();
//
//                 var curveloop1 = upface1.GetEdgesAsCurveLoops().FirstOrDefault();
//                 var curvearray1 = curveloop1.ToCurveArray();
//
//                 var curveloop2 = upface2.GetEdgesAsCurveLoops().FirstOrDefault();
//                 var curvearray2 = curveloop2.ToCurveArray();
//
//                 ActiveDoc.Invoke(m =>
//                 {
//                     var newfloor1 = ActiveDoc.Create.NewFloor(curvearray1, floor.FloorType,
//                                                               floor.LevelId.GetElement(ActiveDoc) as Level, false);
//                     var newfloor2 = ActiveDoc.Create.NewFloor(curvearray2, floor.FloorType,
//                                                               floor.LevelId.GetElement(ActiveDoc) as Level, false);
//                     doc.Delete(floor.Id);
//                 }, "newfloor");
//
//                 if (this != null)
//                     uiapp.Idling -= OnIdling;
//             }
//             catch (Exception e1)
//             {
//                 MessageBox.Show(e1.ToString());
//                 uiapp.Idling -= OnIdling;
//             }
//         }
//
//         private void OnDocumentChanged(object sender, DocumentChangedEventArgs e)
//         {
//             var App = sender as Application;
//
//             try
//             {
//                 var docsInApp = App.Documents.Cast<Document>();
//
//                 var ActiveDoc = e.GetDocument();
//                 ids_add = e.GetAddedElementIds();
//
//                 if (ids_add.Count != 1)
//                 {
//                     return;
//                 }
//
//                 var modelline = ids_add.First().GetElement(ActiveDoc) as ModelLine;
//                 var line = modelline.GeometryCurve as Line;
//
//                 //MessageBox.Show(line.Length.ToString());
//
//                 var linedir = line.Direction;
//                 var startpo = line.StartPoint();
//                 var endpo = line.EndPoint();
//
//                 var updir = XYZ.BasisZ;
//
//                 var leftNorm = updir.CrossProduct(linedir).Normalize();
//                 var rightNorm = updir.CrossProduct(-linedir).Normalize();
//
//                 var leftspacePlane = default(Plane); //Plane.CreateByNormalAndOrigin(leftNorm, startpo);
//                 var rightspacePlane = default(Plane); //PPlane.CreateByNormalAndOrigin(rightNorm, startpo);
//
// #if Revit2016
//                 leftspacePlane = new Plane(leftNorm, startpo);
//                 rightspacePlane = new Plane(rightNorm, startpo);
//
// #endif
// #if Revit2020
//                 leftspacePlane = Plane.CreateByNormalAndOrigin(leftNorm, startpo);
//                 rightspacePlane = Plane.CreateByNormalAndOrigin(rightNorm, startpo);
// #endif
//
//                 var slapshapeEditor = floor.SlabShapeEditor;
//                 //剪切楼板
//
//                 //var verticals = slapshapeEditor.SlabShapeVertices.Cast<XYZ>();
//                 //var upfaceRef = HostObjectUtils.GetTopFaces(floor);
//                 //var upface = floor.GetGeometryObjectFromReference(upfaceRef.FirstOrDefault());
//
//                 var geoele = floor.get_Geometry(new Options()
//                                                     {ComputeReferences = true, DetailLevel = ViewDetailLevel.Fine});
//
//                 var solid = geoele.Getsolids().FirstOrDefault();
//
//                 var newsolid1 = BooleanOperationsUtils.CutWithHalfSpace(solid, leftspacePlane);
//                 var newsolid2 = BooleanOperationsUtils.CutWithHalfSpace(solid, rightspacePlane);
//
//                 var upface1 = newsolid1.GetFacesOfGeometryObject()
//                     .Where(m => m.ComputeNormal(new UV()).IsSameDirection(XYZ.BasisZ))
//                     .FirstOrDefault();
//
//                 var upface2 = newsolid2.GetFacesOfGeometryObject().Where(m => m.ComputeNormal(new UV()) == XYZ.BasisZ)
//                     .FirstOrDefault();
//
//                 var curveloop1 = upface1.GetEdgesAsCurveLoops().FirstOrDefault();
//
//                 var curvearray = curveloop1.ToCurveArray();
//
//                 ActiveDoc.Invoke(m =>
//                 {
//                     var newfloor1 = ActiveDoc.Create.NewFloor(curvearray, floor.FloorType,
//                                                               floor.LevelId.GetElement(ActiveDoc) as Level, false);
//                 }, "newfloor");
//
//                 //var newfloor2 =
//                 //    ActiveDoc.Create.NewFloor(, floor.FloorType, floor.LevelId.GetElement(ActiveDoc) as Level, false);
//
//                 //slapshapeEditor.DrawSplitLine()
//                 if (this != null)
//                     App.DocumentChanged -= OnDocumentChanged;
//             }
//             catch (Exception e1)
//             {
//                 MessageBox.Show(e1.ToString());
//                 App.DocumentChanged -= OnDocumentChanged;
//             }
//         }
//     }
// }