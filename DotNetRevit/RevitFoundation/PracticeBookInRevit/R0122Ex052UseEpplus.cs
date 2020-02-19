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
using System.IO;
using OfficeOpenXml;

namespace ExerciseProject.PracticeBookInRevit
{
    [Transaction((TransactionMode.Manual))]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    class R0122Ex052UseEpplus : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;

            Transaction ts = new Transaction(doc, "**");

            ///[0122练习05] 使用Epplus输出 ( 1月31日):
            /// 过滤出当文件所有的元素。输出每个元素的ID ，Category， 名称， 位置
            ///（分别在项目文档 和 族 文档测试）

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            //获得所有的元素
            BuiltInParameter testParam = BuiltInParameter.ID_PARAM;
            ParameterValueProvider valueProvider = new ParameterValueProvider(new ElementId((int) testParam));
            FilterNumericRuleEvaluator evaluator = new FilterNumericGreater();
            ElementId ruleValue = new ElementId(-1);
            FilterRule elementIdRuleFilter = new FilterElementIdRule(valueProvider, evaluator, ruleValue);
            ElementParameterFilter filter = new ElementParameterFilter(elementIdRuleFilter);
            collector.WherePasses(filter);

            //导出Excel文件路径

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

            //数据临时存储位置
            List<object[]> elementDataList = new List<object[]>();

            // string info = null;
            //
            // info += "元素共有" + collector.Count().ToString() + "个\n\t";

            foreach (Element element in collector)
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

            //保存
            package.Save();
            package.Dispose();

            return Result.Succeeded;
        }
    }
}