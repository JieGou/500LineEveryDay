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
    public class R0212LinqGroupAccumulation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var ele = from e in collector.OfClass(typeof(FamilyInstance)).WhereElementIsNotElementType()
                    .Cast<FamilyInstance>()
                group e by e.Symbol.Family.Name.ToString()
                into g
                select new {count = g.Count(), key = g.Key,};

            foreach (var temp in ele)
            {
                info += temp + "\n";
            }

            //int 累加
            var totalNum = ele.Sum(t => t.count);
            info += "总数为 : " + totalNum +"\n";

            //str 累加
            var totalStr = string.Join(",", ele.Select(m => m.key));
            info += "字符串的累加为 : " + totalStr;

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}