// // using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
// using CurvedBeamWpf.view;
//
//
// namespace CurvedBeamWpf.Command
// {
//     [Transaction(TransactionMode.Manual)]
//     [Regeneration(RegenerationOption.Manual)]
//     public class MainCommand : IExternalCommand
//     {
//         public static Document doc = null;
//
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             UIDocument uidoc = commandData.Application.ActiveUIDocument;
//             doc = uidoc.Document;
//
//             CurvedBeamView mainView = new CurvedBeamView(commandData);
//
//             mainView.ShowDialog();
//             return Result.Succeeded;
//         }
//     }
// }