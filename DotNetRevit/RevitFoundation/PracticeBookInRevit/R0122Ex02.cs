using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa.BinLibrary.Helpers;
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject.PracticeBookInRevit
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class R0122Ex02 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;

            Transaction ts = new Transaction(doc, "**");

            ts.Start();

            /// 1月22日, 练习02:
            /// 在revit中过滤出名称为 标准 的 ElementType的个数

            string info = null;

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var founds = collector.WhereElementIsElementType();
            info += "\n\t其中是ElementType的元素数量是:" + founds.Count().ToString();

            var targetElements = from element in founds
                where element.Name == "标准"
                select element;

            List<Element> bzElements = targetElements.ToList<Element>();

            info = "其中名称为 标准 的元素数量是: " + bzElements.Count.ToString();

            foreach (Element element in bzElements)
            {
                info += "\n\t Id:" + element.Id + ";Name:" + element.Name + "\n\t";
            }

            MessageBox.Show(info);

            ts.Commit();

            return Result.Succeeded;
        }
    }
}