using System;
using System.Collections.Generic;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Plumbing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace RevitDevelopmentFoundation.Epplus
{
    [Transaction(TransactionMode.Manual)]
    class RevitDataToExcelDemo2 : IExternalCommand
    {
        /// <summary>
        /// 此段代码为复制粘贴.
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;

            //Excel文件路径
            string path = @"D:\TestDir1\RevitDataToExcelDemo2.xlsx";

            //如文件已存在则删除
            if (File.Exists(path)) File.Delete(path);

            //创建Excel文件
            ExcelPackage package = new ExcelPackage(new FileInfo(path));
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets.Add("提取柱数据");

            //表头
            string[] headName = {"Id", "Name", "Family"};

            for (int i = 0; i < headName.Length; i++)
            {
                //cells[row,column] ,从1开始计数,从非编程的使用习惯一致.
                ExcelRange hCell = excelWorkSheet.Cells[1, i + 1];
                hCell.Value = headName[i];
            }

            //获得所有柱子
            List<object[]> columnDataList = new List<object[]>();
            FilteredElementCollector collector = new FilteredElementCollector(document);
            var columns = collector.OfCategory(BuiltInCategory.OST_Columns).WhereElementIsNotElementType();
            
            foreach (Element column in columns)
            {
                string columnId, ColumnName, columnFamily;
            
                //读取数据
                columnId = column.Id.ToString();
            
                ColumnName = column.Name;
            
                columnFamily = (column as FamilyInstance).Symbol.FamilyName;
            
                object[] columnData =
                    {columnId, ColumnName, columnFamily};
                columnDataList.Add(columnData);
            }
            
            //写入数据
            for (int i = 0; i < columnDataList.Count; i++)
            {
                object[] columnData = columnDataList[i];
            
                for (int j = 0; j < columnData.Length; j++)
                {
                    ExcelRange dCell = excelWorkSheet.Cells[i + 2, j + 1];
                    dCell.Value = columnData[j];
                }
            }

            //保存
            package.Save();
            package.Dispose();

            return Result.Succeeded;
        }
    }
}

/*
————————————————
版权声明：本文为CSDN博主「imfour」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/imfour/article/details/82959945
在此基础上修改
*/