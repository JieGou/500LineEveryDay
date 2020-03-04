// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit.ApplicationServices;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
// using Autodesk.Revit.UI.Selection;
//
// namespace CodeInTangsengjiewa2.CodeInJeme
// {
//     /// <summary>
//     /// 使用实体相交(Solid Intersection)过滤有接触的梁
//     /// code form:https://blog.csdn.net/flower4wine/article/details/8055279?utm_source=app
//     /// </summary>
//     [Transaction(TransactionMode.Manual)]
//     [Regeneration(RegenerationOption.Manual)]
//     [Journaling(JournalingMode.UsingCommandData)]
//     class Cmd_FilterForTouchingBeamsUsingSolidIntersection : IExternalCommand
//     {
//         public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//         {
//             UIApplication uiapp = commandData.Application;
//             Application app = uiapp.Application;
//             UIDocument uidoc = uiapp.ActiveUIDocument;
//             Document doc = uidoc.Document;
//             Selection sel = uidoc.Selection;
//
//             Autodesk.Revit.Creation.Application createApp = app.Create;
//             Reference r = null;
//
//             try
//             {
//                 r = sel.PickObject(ObjectType.Element, "please select a beam");
//             }
//             catch (RvtOperationCancelledException)
//             {
//                 return Result.Cancelled;
//             }
//
//             //初始梁
//             Element start = doc.GetElement(r);
//             //当前梁表:我们需要查找他们的相邻梁
//             List<ElementId> current = new List<ElementId>();
//             current.Add(start.Id);
//             //已处理的梁表
//             List<ElementId> visited = new List<ElementId>();
//             //相邻梁表
//             List<ElementId> neighbours = new List<ElementId>();
//             //递归调用
//             while (0 < current.Count)
//             {
//                 //记录已处理梁表
//                 visited.AddRange(current);
//                 neighbours.Clear();
//                 //查找当前量表的相邻梁表
//                 foreach (ElementId id in current)
//                 {
//                     Element e = doc.GetElement(id);
//                     AddConnectedElements(neighbours, e, visited);
//                 }
//                 //当前量表处理完毕,找到相邻梁表,成为一下次操作的当前梁表
//                 current.Clear();
//                 current.AddRange(neighbours);
//             }
//             IList<ElementId> ids = new List<ElementId>();
//             foreach (ElementId id in visited)
//             {
//                 ids.Add(id);
//             }
//             uidoc.Selection.SetElementIds(ids);
//
//             return Result.Succeeded;
//         }
//
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="neighbors"></param>
//         /// <param name="e"></param>
//         /// <param name="visited"></param>
//         void AddConnectedElements(List<ElementId> neighbors, Element e, List<ElementId> visited)
//         {
//             Location loc = e.Location;
//             Debug.Print(string.Format("Current element {0} has location {1}",ElementDescription(e),null == loc ? "<null>":loc.GetType().Name));
//             LocationCurve lc = loc as LocationCurve;
//
//             if (null != lc)
//             {
//                 Document doc = e.Document;
//                 Curve c = lc.Curve;
//                 XYZ p = c.GetEndPoint(0);
//                 XYZ q = c.GetEndPoint(1);
//                 AddElementsIntersectingSphereAt(neighbors, p, visited, doc);
//                 AddElementsIntersectingSphereAt(neighbors, q, visited, doc);
//             }
//         }
//
//         void AddElementsIntersectingSphereAt(List<ElementId> neighbors, XYZ p, List<ElementId> visited, Document doc)
//         {
//             Solid sphere= CreateSphereAt()
//         }
//     }
// }