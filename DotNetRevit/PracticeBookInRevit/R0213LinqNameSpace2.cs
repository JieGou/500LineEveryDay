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
    public class R0213LinqNameSpace2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance));

            //查询语法
            var namesQuery = from n in collector
                where n.Name.Length > 1
                select n.Name;

            info += "\n查询语法：  " + namesQuery.Count().ToString();
            foreach (string s in namesQuery)
            {
                info += "\n" + s;
            }

            //命令语法
            var namesMethod = collector
                .Where(x => x.Name.Length > 1)
                .Select(x => x.Name);
            
            info += "\n命令语法:   " +namesMethod.Count().ToString();
            foreach (string s in namesMethod)
            {
                info += "\n" + s;
            }

            //两种形式结合
            var namesCount = (from n in collector
                              where n.Name.Length > 1
                              select n).Count();

            info += "\n数量:  " +namesCount.ToString();


            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}