using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using OfficeOpenXml;

namespace RevitFoundation.PracticeBookInRevit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0205SufferCategory : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;

            //方法1

            // string info = null;
            //
            // foreach (var e in cateValue)
            // {
            //     info += "\n" + e.ToString();
            // }

            var cateValue = Enum.GetValues(typeof(BuiltInCategory));

            #region Excel表格准备

            DateTime dt = DateTime.Now;
            string time = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() +
                          dt.Minute.ToString() + dt.Second.ToString();

            string path1 = @"D:\TestDir1\enumGetValues" + time + ".xlsx";
            ExcelPackage package = new ExcelPackage(new FileInfo(path1));
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets.Add("提取到的元素数据");

            int i = 0;

            foreach (var e in cateValue)
            {
                ExcelRange dCell = excelWorkSheet.Cells[i + 1, 1];
                dCell.Value = e;
                i++;
            }

            //保存: 很重要
            package.Save();
            package.Dispose();

            #endregion

            // info += "\n\n\n\n" + "*****************";
            //
            // foreach (var e in cateNames)
            // {
            //     info += "\n" + e;
            // }
            //
            // TaskDialog.Show("tip", info);

            var cateNames = Enum.GetNames(typeof(BuiltInCategory));

            #region Excel表格准备2

            string path2 = @"D:\TestDir1\enumGetNames" + time + ".xlsx";
            ExcelPackage package2 = new ExcelPackage(new FileInfo(path2));
            ExcelWorksheet excelWorkSheet2 = package2.Workbook.Worksheets.Add("提取到的元素数据");

            int j = 0;

            foreach (var e in cateNames)
            {
                ExcelRange dCell = excelWorkSheet2.Cells[j + 1, 1];
                dCell.Value = e.ToString();
                j++;
            }

            //保存: 很重要
            package2.Save();
            package2.Dispose();

            #endregion

            return Result.Succeeded;
        }
    }
}