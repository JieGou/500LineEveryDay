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
    public class R0212LinqGroup2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            //分组
            //排序
            //去重 的代码
            var ele = collector
                .WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_Doors)
                .Cast<FamilyInstance>()
                .GroupBy(p => p.Name)
                .Select(g => g.FirstOrDefault())
                .ToList()
                .OrderByDescending( m =>m.Name);

            // //不去重的代码
            // var ele = collector
            //     .WhereElementIsNotElementType()
            //     .OfClass(typeof(FamilyInstance))
            //     .OfCategory(BuiltInCategory.OST_Doors)
            //     .Cast<FamilyInstance>()
            //     .Select(p => p)
            //     .ToList();

            foreach (var temp in ele)
            {
                info += temp.Name + "\n";
            }

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}