// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.UI;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI.Events;
// using Autodesk.Revit.UI.Selection;
// using System.Windows;
// namespace ExerciseProject
// {
//     [Transaction(TransactionMode.Manual)]
//     [Regeneration(RegenerationOption.Manual)]
//     [Journaling(JournalingMode.UsingCommandData)]
//     class CmdTemplateTool : IExternalCommand
//     {
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             UIApplication uiapp = commandData.Application;
//             UIDocument uidoc = commandData.Application.ActiveUIDocument;
//             Document doc = uidoc.Document;
//             Selection sel = uidoc.Selection;
//             View acView = doc.ActiveView;
//            
//             return Result.Succeeded;
//         }
//
//         private void CreateAdaptiveComponentFamily(Document document)
//         {
//
//             Transaction transaction = new Transaction(document);
//
//             transaction.Start("创建线");
//             ReferencePointArray refPointArray = new ReferencePointArray();
//             for (int i = 0; i < ListData.ListX.Count; i++)
//             {
//
//                 ReferencePoint referencePoint = document.FamilyCreate.NewReferencePoint(new XYZ(ListData.ListX[i] / 304.8, ListData.ListY[i] / 304.8, ListData.ListZ[i] / 304.8));
//
//                 refPointArray.Append(referencePoint);
//             }
//
//             CurveByPoints curve = document.FamilyCreate.NewCurveByPoints(refPointArray);
//             transaction.Commit();
//         }
//     }
// }