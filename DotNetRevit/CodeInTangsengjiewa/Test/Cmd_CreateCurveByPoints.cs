// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit.DB;
// using Autodesk.Revit;
// using Autodesk.Revit.UI;
//
// namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.Test
// {
//     class Cmd_CreateCurveByPoints : IExternalCommand
//
//     {
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             return Result.Succeeded;
//         }
//
//         public void CreateAdaptiveComponentFamily(Document document)
//         {
//             Transaction transaction = new Transaction(document);
//
//             transaction.Start("创建线");
//             ReferencePointArray refPointArray = new ReferencePointArray();
//           //  var ListData =new ;
//             for (int i = 0; i < ListData.ListX.Count; i++)
//             {
//                 ReferencePoint referencePoint =
//                     document.FamilyCreate.NewReferencePoint(new XYZ(ListData.ListX[i] / 304.8,
//                                                                     ListData.ListY[i] / 304.8,
//                                                                     ListData.ListZ[i] / 304.8));
//
//                 refPointArray.Append(referencePoint);
//             }
//
//             CurveByPoints curve = document.FamilyCreate.NewCurveByPoints(refPointArray);
//             transaction.Commit();
//         }
//     }
// }