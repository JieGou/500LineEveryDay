using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    class R0202Ex01SuferCategoryInDoc : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;

            Transaction ts = new Transaction(doc, "**");

            ///0202作业01:
            ///遍历文档中所有的Category

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            //获得所有的元素
            BuiltInParameter testParam = BuiltInParameter.ID_PARAM;
            ParameterValueProvider valueProvider = new ParameterValueProvider(new ElementId((int)testParam));
            FilterNumericRuleEvaluator evaluator = new FilterNumericGreater();
            ElementId ruleValue = new ElementId(-1);
            FilterRule elementIdRuleFilter = new FilterElementIdRule(valueProvider, evaluator, ruleValue);
            ElementParameterFilter filter = new ElementParameterFilter(elementIdRuleFilter);
            collector.WherePasses(filter);

            //xu add
            //method 1
            var wallcate1 = Category.GetCategory(doc, BuiltInCategory.OST_Walls);

            var catevalues = Enum.GetValues(typeof(BuiltInCategory));
            foreach (object item in catevalues)
            {
                var builtincate = (BuiltInCategory) item;
                var aaa = Enum.GetName(typeof(BuiltInCategory), item);
            }

            //method 2
            var caties = doc.Settings.Categories;
            var wallCate = caties.Cast<Category>()
                .FirstOrDefault(m => m.Id.IntegerValue == (int)BuiltInCategory.OST_Walls);

            //for test
            caties.Cast<Category>(). GroupBy(m => m.Name.Contains("aa"));



            //导出Excel文件路径

            DateTime dt = DateTime.Now;
            string time = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() +
                          dt.Minute.ToString() + dt.Second.ToString();

            string path = null;

            // if (!doc.IsFamilyDocument)
            // {
            path = @"D:\TestDir1\CategoryInDoc" + time + ".xlsx";
            // }
            // else
            // {
            //     path = @"D:\TestDir1\elementInFamily" + time + ".xlsx";
            // }

            //如文件已存在则删除
            if (File.Exists(path)) File.Delete(path);
            //创建Excel文件
            ExcelPackage package = new ExcelPackage(new FileInfo(path));
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets.Add("文档中的Category列表");

            //表头
            string[] headName = { "Id", "Category" };

            for (int i = 0; i < headName.Length; i++)
            {
                //cells[row,column] ,从1开始计数,从非编程的使用习惯一致.
                ExcelRange hCell = excelWorkSheet.Cells[1, i + 1];
                hCell.Value = headName[i];
            }

            //数据临时存储位置
            List<object[]> elementDataList = new List<object[]>();

            int ID = 0;

            foreach (Element element in collector)
            {
                string category;

                // 读取数据
                ID++;

                if (null == element.Category)
                {
                    category = "null";
                }
                else
                {
                    category = element.Category.Name;
                }

                object[] elementData = { ID.ToString(), category };
                elementDataList.Add(elementData);
            }

            //去重 ??? 没搞出来,用excel表的去重功能完成的.


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