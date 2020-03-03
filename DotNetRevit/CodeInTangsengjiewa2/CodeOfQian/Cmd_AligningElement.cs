// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows;
// using System.Xml;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.DB.Visual;
// using Autodesk.Revit.UI;
// using Autodesk.Revit.UI.Selection;
// using CodeInTangsengjiewa2.BinLibrary.Extensions;
// using CodeInTangsengjiewa2.BinLibrary.Helpers;
// using CodeInTangsengjiewa2.通用.UIs;
//
//
// namespace CodeInTangsengjiewa2.CodeOfQian
// {
//     /// <summary>
//     /// what can i do with revit api now?
//     /// aligning Element: 将元素复制到指定的位置
//     /// Code From SDK:CreateTruss: this class create a mono truss in truss family document
//     /// </summary>
//     [Transaction(TransactionMode.Manual)]
//     [Regeneration(RegenerationOption.Manual)]
//     [Journaling(JournalingMode.UsingCommandData)]
//     public class Cmd_AligningElement : IExternalCommand
//     {
//         private static Autodesk.Revit.ApplicationServices.Application m_applicaiton;
//
//         private static Autodesk.Revit.DB.Document m_document;
//
//         private Autodesk.Revit.Creation.Application m_appCreator;
//
//         private Autodesk.Revit.Creation.FamilyItemFactory m_familyCreator;
//
//
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             try
//             {
//                 m_applicaiton = commandData.Application.Application;
//                 m_document = commandData.Application.ActiveUIDocument.Document;
//
//                 //it can support in truss family document only
//                 if (!m_document.IsFamilyDocument
//                     || m_document.OwnerFamily.FamilyCategory.Id.IntegerValue != (int) BuiltInCategory.OST_Truss)
//                 {
//                     message = " cannot execute truss creation in non-truss family document";
//                     return Result.Failed;
//                 }
//
//                 m_appCreator = m_applicaiton.Create;
//                 m_familyCreator = m_document.FamilyCreate;
//
//                 Transaction newTran = new Transaction(m_document);
//                 newTran.Start("NewTrussCurve");
//                 //start the truss creation
//                 MakeNewTruss();
//                 newTran.Commit();
//             }
//             catch (Exception e)
//             {
//                 message = e.ToString();
//                 return Result.Failed;
//             }
//
//             return Result.Succeeded;
//         }
//
//         /// <summary>
//         /// example demonstrating truss creation in API.
//         /// this example constructs a mono truss alignen with the reference planes in the (already loaded)truss family document
//         /// </summary>
//         private void MakeNewTruss()
//         {
//             //constants for arranging th angular truss members
//             double webAngle = 35.0;
//             double webAngleRadius = (180 - webAngle) * Math.PI / 180;
//             XYZ angleDirection = new XYZ(Math.Cos(webAngleRadius), Math.Sin(webAngleRadius), 0);
//
//             //look up  the  reference planes and view in which to sketch
//             ReferencePlane top = null, bottom = null, left = null, right = null, center = null;
//             View level1 = null;
//             List<Element> elements = new List<Element>();
//             ElementClassFilter refPlaneFilter = new ElementClassFilter(typeof(ReferencePlane));
//             ElementClassFilter viewFilter = new ElementClassFilter(typeof(View));
//             LogicalOrFilter filter = new LogicalOrFilter(refPlaneFilter, viewFilter);
//             FilteredElementCollector collector = new FilteredElementCollector(m_document);
//
//             elements.AddRange(collector.WherePasses(filter).ToElements());
//
//             foreach (Element e in elements)
//             {
//                 //skip view template because they are invisible for truss creation
//                 View view = e as View;
//                 if (null != view && view.IsTemplate)
//                 {
//                     continue;
//                 }
//                 switch (e.Name)
//                 {
//                     case "Top":
//                         top = e as ReferencePlane;
//                         break;
//                     case "Bottom":
//                         bottom = e as ReferencePlane;
//                         break;
//                     case "Right":
//                         right = e as ReferencePlane;
//                         break;
//                     case "Left":
//                         left = e as ReferencePlane;
//                         break;
//                     case "Center":
//                         center = e as ReferencePlane;
//                         break;
//                     case "Level1":
//                         level1 = e as View;
//                         break;
//                 }
//             }
//             if (top == null || bottom == null ||left ==null ||right ==null || center==null || level1 ==null)
//             {
//                 throw  new InvalidOperationException("could not find prerequisite named reference plane or named view ");
//             }
//
//             SketchPlane sPlane = level1.SketchPlane;
//
//             //extract the geometry of each reference plane
//
//
//         }
//     }
// }