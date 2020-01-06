// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
//
// namespace ExerciseProject.AutodeskDevelopmentFoundation
// {
//     class tem1:IExternalCommand
//     {
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             var doc = commandData.Application.ActiveUIDocument.Document;
//             Reference ref1 = null;
//
//             FamilyInstance fi = doc.GetElement(ref1) as FamilyInstance ;
//             Instance ins = fi;
//             Element ele = ins;
//
//             Element ele2 = fi;
//
//             IExternalCommand cmd = new tem1();
//
//             IList<XYZ> a = new List<XYZ>();
//             List<XYZ> b = new List<XYZ>();
//
//             b = a.ToList();
//             a = b;
//
//
//             return Result.Succeeded;
//         }
//     }
// }
