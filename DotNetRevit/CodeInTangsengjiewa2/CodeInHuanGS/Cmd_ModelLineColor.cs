using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace CodeInTangsengjiewa2.CodeInHuanGS
{
    /// <summary>
    /// Revit开发设置模型线颜色:
    /// Revit里要设置模型线的颜色有2中方法 
    /// 一种是设置线的样式 
    /// 一种是设置OverrideGraphicSettings 
    /// 第一种需要设置新建线的样式， 
    /// 然后通过模型线的线样式参数设置 
    /// 第二种是通过替换视图中的图形，它是 
    /// 基于视图的，颜色只在当前视图有效 
    /// code from: https://blog.csdn.net/the_eyes/article/details/52740095
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_ModelLineColor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = doc.ActiveView;

            //方法一: 新建线样式
            Category tCat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element elem = doc.GetElement(reference);
            Transaction ts = new Transaction(doc, "Trans");
            ts.Start();

            if (!tCat.SubCategories.Contains("MyLine"))
            {
                Category nCat = doc.Settings.Categories.NewSubcategory(tCat, "MyLine");
                nCat.LineColor = new Color(255, 0, 0);
            }
            doc.Regenerate();
            FilteredElementCollector temCollector = new FilteredElementCollector(doc);
            GraphicsStyle mgs =
                temCollector.OfClass(typeof(GraphicsStyle)).First(m => (m as GraphicsStyle).GraphicsStyleCategory.Name == "MyLine") as GraphicsStyle;
            Parameter temParameter = elem.LookupParameter("线样式");
            temParameter.Set(mgs.Id);

            // //方法二:替换视图中的图形
            // Reference r = uidoc.Selection.PickObject(ObjectType.Element);
            // Element elem = doc.GetElement(r);
            // OverrideGraphicSettings ogs = view.GetElementOverrides(elem.Id);
            // Transaction ts = new Transaction(doc, "Trans");
            // ts.Start();
            // ogs.SetProjectionLineColor(new Color(0, 255, 0));
            // view.SetElementOverrides(elem.Id, ogs);

            ts.Commit();

            return Result.Succeeded;
        }
    }
}