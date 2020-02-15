using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace RevitDevelopmentFoudation.PracticeBookInRevit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class R0213LinqNameSpaceDetegate : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.WhereElementIsNotElementType();

            var numAll = collector
                .OfClass(typeof(FamilyInstance))
                .Count();
            info += numAll + "\n";

            var numOfColumn = collector
                .OfCategory(BuiltInCategory.OST_Columns)
                .OfClass(typeof(FamilyInstance))
                .Count();
            info += numOfColumn + "\n";

            //使用委托的实例
            Func<Element, bool> myDel = new Func<Element, bool>(IsFamilyInstanceAndColumn);
            //Element 是 in
            //bool 是 out
            var numOfColumn2 = collector.Count(myDel);
            info += numOfColumn2 + "\n";

            //使用lamda表达式的实例
            var numofColumn3 =
                collector.Count(x => x is FamilyInstance &&
                                     x.Category.Id == new ElementId(BuiltInCategory.OST_Columns));

            info += numofColumn3 + "\n";

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }

        //需要委托的方法
        static bool IsFamilyInstanceAndColumn(Element e)
        {
            //doc 怎么作为参数写进来
            return (e is FamilyInstance) && (e.Category.Id == new ElementId(BuiltInCategory.OST_Columns));
        }
    }
}