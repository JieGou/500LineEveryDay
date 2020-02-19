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
    public class R0213LinqNameSpace : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string info = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);

     
            collector.WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance));

            var query = from element in collector
                where element.Name == "60 x 30 Student"
                select element;

            List<FamilyInstance> familyInstances = query.Cast<FamilyInstance>().ToList<FamilyInstance>();

            foreach (FamilyInstance instance in familyInstances)
            {
                info += "\n坐标为：\nX：" + (instance.Location as LocationPoint).Point.X.ToString()
                                   + "\nY：" + (instance.Location as LocationPoint).Point.Y.ToString();
            }
          

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}