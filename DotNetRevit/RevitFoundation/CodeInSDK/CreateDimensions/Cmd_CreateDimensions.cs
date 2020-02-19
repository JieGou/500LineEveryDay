// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
// using Autodesk.Revit;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB.Structure;
//
//

///**************************
///未能正确运行, sdk也的也不能正确运行.
/// **************************
///
/// 
// namespace RevitDevelopmentFoudation.CodeInSDK.CreateDimensions
// {
//     [Transaction(TransactionMode.Manual)]
//     [Regeneration(RegenerationOption.Manual)]
//     [Journaling(JournalingMode.UsingCommandData)]
//     public class Cmd_CreateDimensions : IExternalCommand
//     {
//         private ExternalCommandData m_revit = null; //stroe external command
//         private string m_errorMessage = ""; //store error message
//         ArrayList m_walls = new ArrayList(); //store the wall of selected
//         private const double precision = 0.000001; //store the precision
//
//         public Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
//         {
//             m_revit = revit;
//             View view = m_revit.Application.ActiveUIDocument.Document.ActiveView;
//             View3D view3d = view as View3D;
//
//             if (null != view3d)
//             {
//                 message += "only create dimessions in 2d";
//                 return Result.Failed;
//             }
//
//             ViewSheet viewSheet = view as ViewSheet;
//
//             if (null != viewSheet)
//             {
//                 message += "only create dimensions in 2d";
//                 return Result.Failed;
//             }
//
//             //try to adds a dimension from the start of the wall to the end of the wall into the project
//             // if (!AddDimension())
//             // {
//             //     message = m_errorMessage;
//             //     return Result.Failed;
//             // }
//
//             if (true == initailize())
//             {
//                 Transaction ts = new Transaction(m_revit.Application.ActiveUIDocument.Document, "Add Dimension");
//                 ts.Start();
//
//                 //get out all walls in the array,and create a dimension from its start to its end
//                 for (int i = 0; i < m_walls.Count; i++)
//                 {
//                     Wall wallTemp = m_walls[i] as Wall;
//
//                     if (null == wallTemp)
//                     {
//                         continue;
//                     }
//
//                     //get location curve
//                     Location location = wallTemp.Location;
//                     LocationCurve locationline = location as LocationCurve;
//
//                     if (null == locationline)
//                     {
//                         continue;
//                     }
//
//                     //new line
//                     Line newLine = null;
//                     //get reference
//                     ReferenceArray referenceArray = new ReferenceArray();
//
//                     AnalyticalModel analyticalModel = wallTemp.GetAnalyticalModel();
//                     // IList<Curve> activeCurveList = analyticalModel.GetCurves(AnalyticalCurveType.ActiveCurves);
//                     IList<Curve> activeCurveList = analyticalModel.GetCurves(AnalyticalCurveType.ActiveCurves);
//
//                     foreach (Curve aCurve in activeCurveList)
//                     {
//                         //find non-vertical curve from analytical model
//                         if (aCurve.GetEndPoint(0).Z == aCurve.GetEndPoint(1).Z)
//                         {
//                             newLine = aCurve as Line;
//                         }
//
//                         if (aCurve.GetEndPoint(0).Z != aCurve.GetEndPoint(1).Z)
//                         {
//                             AnalyticalModelSelector amSelector = new AnalyticalModelSelector(aCurve);
//                             amSelector.CurveSelector = AnalyticalCurveSelector.StartPoint;
//                             referenceArray.Append(analyticalModel.GetReference(amSelector));
//                         }
//
//                         if (2 == referenceArray.Size)
//                         {
//                             break;
//                         }
//                     }
//
//                     if (referenceArray.Size != 2)
//                     {
//                         m_errorMessage += "Did not find two references";
//                     }
//
//                     try
//                     {
//                         //try to add new a dimension
//                         Autodesk.Revit.UI.UIApplication app = m_revit.Application;
//                         Document doc = app.ActiveUIDocument.Document;
//
//                         XYZ p1 = new XYZ(
//                                          newLine.GetEndPoint(0).X + 5,
//                                          newLine.GetEndPoint(0).Y + 5,
//                                          newLine.GetEndPoint(0).Z);
//                         XYZ p2 = new XYZ(
//                                          newLine.GetEndPoint(1).X + 5,
//                                          newLine.GetEndPoint(1).Y + 5,
//                                          newLine.GetEndPoint(1).Z);
//                         Line newLine2 = Line.CreateBound(p1, p2);
//                         Dimension newDimension = doc.Create.NewDimension(doc.ActiveView, newLine2, referenceArray);
//                     }
//                     catch (Exception e)
//                     {
//                         m_errorMessage += e.ToString();
//                     }
//                 }
//
//                 ts.Commit();
//             }
//
//             return Result.Succeeded;
//         }
//
//         /// <summary>
//         /// find out the wall , insert it into a array list
//         /// </summary>
//         /// <returns></returns>
//         bool initailize()
//         {
//             ElementSet selections = new ElementSet();
//
//             foreach (ElementId elementId in m_revit.Application.ActiveUIDocument.Selection.GetElementIds())
//             {
//                 selections.Insert(m_revit.Application.ActiveUIDocument.Document.GetElement(elementId));
//             }
//
//             //nothing wa selected
//             if (0 == selections.Size)
//             {
//                 m_errorMessage += "please select basic walls";
//                 return false;
//             }
//
//             //find out wall
//             foreach (Element e in selections)
//             {
//                 Wall wall = e as Wall;
//
//                 if (null != wall)
//                 {
//                     if ("Basic" != wall.WallType.Kind.ToString())
//                     {
//                         continue;
//                     }
//
//                     m_walls.Add(wall);
//                 }
//             }
//
//             // no wall was selected
//             if (0 == m_walls.Count)
//             {
//                 m_errorMessage += "please select basic walls";
//                 return false;
//             }
//
//             return true;
//         }
//
//         /// <summary>
//         /// find out every wall in the selection and add a dimension from the start of the wall to its end
//         /// </summary>
//         /// <returns>if add successfully, true will be returned ,else false will be returned.</returns>
//         public bool AddDimension()
//         {
//             if (!initailize())
//             {
//                 return false;
//             }
//
//             Transaction ts = new Transaction(m_revit.Application.ActiveUIDocument.Document, "Add Dimension");
//             ts.Start();
//
//             //get out all walls in the array,and create a dimension from its start to its end
//             for (int i = 0; i < m_walls.Count; i++)
//             {
//                 Wall wallTemp = m_walls[i] as Wall;
//
//                 if (null == wallTemp)
//                 {
//                     continue;
//                 }
//
//                 //get location curve
//                 Location location = wallTemp.Location;
//                 LocationCurve locationline = location as LocationCurve;
//
//                 if (null == locationline)
//                 {
//                     continue;
//                 }
//
//                 //new line
//                 Line newLine = null;
//                 //get reference
//                 ReferenceArray referenceArray = new ReferenceArray();
//
//                 AnalyticalModel analyticalModel = wallTemp.GetAnalyticalModel();
//                 // IList<Curve> activeCurveList = analyticalModel.GetCurves(AnalyticalCurveType.ActiveCurves);
//                 IList<Curve> activeCurveList = analyticalModel.GetCurves(AnalyticalCurveType.ActiveCurves);
//
//                 foreach (Curve aCurve in activeCurveList)
//                 {
//                     //find non-vertical curve from analytical model
//                     if (aCurve.GetEndPoint(0).Z == aCurve.GetEndPoint(1).Z)
//                     {
//                         newLine = aCurve as Line;
//                     }
//
//                     if (aCurve.GetEndPoint(0).Z != aCurve.GetEndPoint(1).Z)
//                     {
//                         AnalyticalModelSelector amSelector = new AnalyticalModelSelector(aCurve);
//                         amSelector.CurveSelector = AnalyticalCurveSelector.StartPoint;
//                         referenceArray.Append(analyticalModel.GetReference(amSelector));
//                     }
//
//                     if (2 == referenceArray.Size)
//                     {
//                         break;
//                     }
//                 }
//
//                 if (referenceArray.Size != 2)
//                 {
//                     m_errorMessage += "Did not find two references";
//                     return false;
//                 }
//
//                 try
//                 {
//                     //try to add new a dimension
//                     Autodesk.Revit.UI.UIApplication app = m_revit.Application;
//                     Document doc = app.ActiveUIDocument.Document;
//
//                     XYZ p1 = new XYZ(
//                                      newLine.GetEndPoint(0).X + 5,
//                                      newLine.GetEndPoint(0).Y + 5,
//                                      newLine.GetEndPoint(0).Z);
//                     XYZ p2 = new XYZ(
//                                      newLine.GetEndPoint(1).X + 5,
//                                      newLine.GetEndPoint(1).Y + 5,
//                                      newLine.GetEndPoint(1).Z);
//                     Line newLine2 = Line.CreateBound(p1, p2);
//                     Dimension newDimension = doc.Create.NewDimension(doc.ActiveView, newLine2, referenceArray);
//                 }
//                 catch (Exception e)
//                 {
//                     m_errorMessage += e.ToString();
//                     return false;
//                 }
//             }
//
//             ts.Commit();
//             return true;
//         }
//     }
// }