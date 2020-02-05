using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using OfficeOpenXml;
using Transaction = Autodesk.Revit.DB.Transaction;


namespace RevitDevelopmentFoudation.PracticeBookInRevit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class R0205SufferElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = commandData.Application.ActiveUIDocument.Document;
            var sel = uidoc.Selection;

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            List<object[]> elementDataList = new List<object[]>();

            foreach (var element in collector.WhereElementIsElementType())
            {
                string elementId, name, category, location;
                // 读取数据
                elementId = element.Id.ToString();
                name = element.Name;

                if (null == element.Category)
                {
                    category = "null";
                }
                else
                {
                    category = element.Category.Name;
                }

                if (null == element.Location)
                {
                    location = "没有位置";
                }
                else
                {
                    location = element.Location.ToString();
                }

                object[] elementData = {elementId, name, category, location};
                elementDataList.Add(elementData);
            }

            foreach (var element in collector.WhereElementIsNotElementType())
            {
                string elementId, name, category, location;
                // 读取数据
                elementId = element.Id.ToString();
                name = element.Name;

                if (null == element.Category)
                {
                    category = "null";
                }
                else
                {
                    category = element.Category.Name;
                }

                if (null == element.Location)
                {
                    location = "没有位置";
                }
                else
                {
                    location = element.Location.ToString();
                }

                object[] elementData = {elementId, name, category, location};
                elementDataList.Add(elementData);
            }

            //导出Excel文件路径

            #region Excel表格准备

            DateTime dt = DateTime.Now;
            string time = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() +
                          dt.Minute.ToString() + dt.Second.ToString();

            string path = null;

            if (!doc.IsFamilyDocument)
            {
                path = @"D:\TestDir1\elementInProject" + time + ".xlsx";
            }
            else
            {
                path = @"D:\TestDir1\elementInFamily" + time + ".xlsx";
            }

            //如文件已存在则删除
            if (File.Exists(path)) File.Delete(path);
            //创建Excel文件
            ExcelPackage package = new ExcelPackage(new FileInfo(path));
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets.Add("提取到的元素数据");

            //表头
            string[] headName = {"ElementId", "Name", "Category", "Location"};

            // string[] headName = { "ElementId", "Category", "Name", "Location", "元素总个数" };
            for (int i = 0; i < headName.Length; i++)
            {
                //cells[row,column] ,从1开始计数,从非编程的使用习惯一致.
                ExcelRange hCell = excelWorkSheet.Cells[1, i + 1];
                hCell.Value = headName[i];
            }

            #endregion

            //写入数据
            for (int i = 0; i < elementDataList.Count; i++)
            {
                object[] elementData = elementDataList[i];

                for (int j = 0; j < elementData.Length; j++)
                {
                    ExcelRange dCell = excelWorkSheet.Cells[i + 2, j + 1];
                    dCell.Value = elementData[j];
                }
            }

            //保存: 很重要
            package.Save();
            package.Dispose();

            Transaction ts = new Transaction(doc, "***********");
            ts.Start();
            ts.Commit();
            return Result.Succeeded;
        }
    }
}