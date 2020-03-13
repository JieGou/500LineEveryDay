using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;


namespace RevitFoundation.PracticeBookInRevit
{
    /// <summary>
    /// 2020年3月13日18:41:58 作业: 改变元素类型。
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class R0313ChangeElementType : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = commandData.Application.ActiveUIDocument.Document;
            var sel = commandData.Application.ActiveUIDocument.Selection;

            //获得元素
            var eleReference = sel.PickObject(ObjectType.Element, "请选择一个元素");
            var elementPicked = eleReference.GetElement(doc);

            if (elementPicked is FamilyInstance)
            {
                //得到选中元素的族的所有族类型(FamilySymbol);
                var targetFamily = (elementPicked as FamilyInstance).Symbol.Family;
                var collector = new FilteredElementCollector(doc);
                var symbols = collector.WhereElementIsElementType().Where(m => m is FamilySymbol)
                    .Where(m => (m as FamilySymbol).Family.Name == targetFamily.Name)
                    .Cast<FamilySymbol>().ToList();

                ////测试是否能找到族下面所有的类型.
                // string info = null;
                // foreach (var familySymbol in symbols)
                // {
                //     info += familySymbol.Name +"\n";
                // }
                // MessageBox.Show(info);

                //改变当前选择的族的类型
                //修改后的元素族类型为: 
                var targetSymbol = symbols.Where(m => m.Name != (elementPicked as FamilyInstance).Symbol.Name)?.First();
                //修改族类型
                doc.Invoke(m => { (elementPicked as FamilyInstance).ChangeTypeId(targetSymbol.Id); },
                           "change FamilySymbol");
            }
            else
            {
                MessageBox.Show("选个FamilyInstance吧, 系统族太难了");
            }

            return Result.Succeeded;
        }
    }
}