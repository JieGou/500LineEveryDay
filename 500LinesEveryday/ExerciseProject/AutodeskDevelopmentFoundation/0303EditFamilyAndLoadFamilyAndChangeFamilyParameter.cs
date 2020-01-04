// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Security.AccessControl;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Forms;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.DB.Structure;
// using Autodesk.Revit.UI;
// using Autodesk.Revit.UI.Selection;
// using TeacherTangClass;
// using View = Autodesk.Revit.DB.View;
//
// namespace ExerciseProject
// {
//★★照个网页的代码敲了一遍, 代码没看懂,没运行起来.
//https://www.cnblogs.com/greatverve/archive/2011/08/31/get-family-from-familyinstance.html
//     [Transaction(TransactionMode.Manual)]
//     [Regeneration(RegenerationOption.Manual)]
//     [Journaling(JournalingMode.UsingCommandData)]
//
//     public class MyFamilyLoadOptions : IFamilyLoadOptions
//     {
//         bool IFamilyLoadOptions.OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
//         {
//             overwriteParameterValues = false;
//             return true;
//         }
//
//         bool IFamilyLoadOptions.OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
//         {
//             source = FamilySource.Project;
//             overwriteParameterValues = true;
//             return true;
//         }
//     }
//
//     class _0303EditFamilyAndLoadFamilyAndChangeFamilyParameter : IExternalCommand
//     {
//         //毫米到英寸,单位换算
//         public double mmToFeet(double val)
//         {
//             return val / 304.8;
//         }
//
//
//         //FindSpecialElements
//         public IList<Element> FindSpecialElements(Document doc, Type targetType, BuiltInCategory category)
//         {
//             FilteredElementCollector collector = new FilteredElementCollector(doc);
//             collector.OfClass(targetType);
//             if (collector != null)
//             {
//                 collector.OfCategory(category);
//             }
//
//             IList<Element> elements = collector.ToElements();
//             return elements;
//         }
//
//         public Element FindElementType(Document doc, Type targetType, string familyName, string typeName,
//             BuiltInCategory category)
//         {
//             IList<Element> elements = FindSpecialElements(doc, targetType, category);
//             Element elem = null;
//             foreach (Element e in elements)
//             {
//                 if (e.Name.Equals((typeName)) &&
//                     e.get_Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString().Equals(familyName))
//                 {
//                     elem = e;
//                     break;
//                 }
//             }
//             return elem;
//         }
//
//         
//
//         Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             UIDocument uidoc = commandData.Application.ActiveUIDocument;
//             Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
//
//             // HyRevit.Key.Press.keys("DR"); //模拟键盘输入dr
//             string family = "单开门";
//             string type = "M0721";
//             FamilySymbol fs = FindElementType(uidoc.Document, typeof(FamilySymbol),
//                 family, type, BuiltInCategory.OST_Doors) as FamilySymbol;
//
//             Family fy = fs.Family;
//
//             Document familyDoc = uidoc.Document.EditFamily(fy);
//             if (null != familyDoc)
//             {
//                 if (familyDoc.IsFamilyDocument)
//                 {
//                     FamilyManager familyManager = familyDoc.FamilyManager;
//                     if (null != familyManager)
//                     {
//                         Transaction ts = new Transaction(familyDoc, "creatDoorFamilyTranscation");
//                         ts.Start();
//
//                         SubTransaction sunSubTransaction = new SubTransaction(familyDoc);
//                         sunSubTransaction.Start();
//
//                         FamilyType newFamilyType = familyManager.NewType("新门1028");
//                         FamilyParameter familyParameter = familyManager.get_Parameter("Width");
//                         if (null != familyParameter)
//                         {
//                             familyManager.Set(familyParameter, mmToFeet(Convert.ToDouble("1000")));
//                         }
//
//                         familyParameter = familyManager.get_Parameter("Height");
//                         if (null != familyParameter)
//                         {
//                             familyManager.Set(familyParameter, mmToFeet(Convert.ToDouble("2800")));
//                         }
//
//                         MyFamilyLoadOptions myOptions = new MyFamilyLoadOptions();
//                         familyDoc.LoadFamily(uidoc.Document,myOptions);
//                         //familyDoc.LoadFamily(uiDoc.Document, new FamilyOptions()); 这个不能运行,显红
//                         sunSubTransaction.Commit();
//                         ts.Commit();
//                         familyDoc.Close(false);
//                         familyDoc.Dispose();
//                     }
//                 }
//             }
//
//             //进入命令, 设置默认
//             family = "单开门";
//             type = "新门1028";
//             FamilySymbol fsNew = FindElementType
//             (uidoc.Document, typeof(FamilySymbol),
//                 family, type, BuiltInCategory.OST_Doors) as FamilySymbol;
//             uidoc.PromptForFamilyInstancePlacement(fsNew);
//
//             return Result.Succeeded;
//         }
//     }
// }