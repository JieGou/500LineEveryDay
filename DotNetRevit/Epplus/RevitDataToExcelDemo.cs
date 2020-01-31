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
    class RevitDataToExcelDemo1 : IExternalCommand
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
            string path = @"D:\TestDir1\RevitDataToExcelDemo1.xlsx";

            //如文件已存在则删除
            if (File.Exists(path)) File.Delete(path);

            //创建Excel文件
            ExcelPackage package = new ExcelPackage(new FileInfo(path));
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets.Add("管道数据");

            //表头
            string[] hearName = { "Id", "系统", "项目名称", "材质", "规格", "连接方式", "单位", "工程量" };
            for (int i = 0; i < hearName.Length; i++)
            {
                ExcelRange hCell = excelWorkSheet.Cells[1, i + 1];
                hCell.Value = hearName[i];

                //格式
                hCell.Style.Font.Bold = true;
                hCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            //获得所有管道数据
            List<object[]> pipeDataList = new List<object[]>();
            FilteredElementCollector collector = new FilteredElementCollector(document);
            foreach (Pipe p in collector.OfClass(typeof(Pipe)).WhereElementIsNotElementType())
            {
                string pipeId, pipeSys, pipeItemName, pipeSize, pipeMaterial, pipeConnect, pipeUnit;
                double pipeQuantity;

                //系统缩写
                string abbr = p.get_Parameter(BuiltInParameter.RBS_DUCT_PIPE_SYSTEM_ABBREVIATION_PARAM).AsString();
                //读取数据
                pipeId = p.Id.ToString();
                pipeSys = GetPipeSys(abbr);
                pipeItemName = p.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsValueString().Split('_')[1];
                pipeSize = p.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString().Split(' ')[0];
                pipeMaterial = GetPipeMaterial(Convert.ToDouble(pipeSize), abbr);
                pipeConnect = GetPipeConnect(Convert.ToDouble(pipeSize), pipeMaterial);
                pipeUnit = "m";
                pipeQuantity = UnitUtils.ConvertFromInternalUnits(p.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), DisplayUnitType.DUT_METERS);

                object[] pipeData = { pipeId, pipeSys, pipeItemName, pipeMaterial, "DN" + pipeSize, pipeConnect, pipeUnit, pipeQuantity };
                pipeDataList.Add(pipeData);
            }

            //写入数据
            for (int i = 0; i < pipeDataList.Count; i++)
            {
                object[] pipeData = pipeDataList[i];
                for (int j = 0; j < pipeData.Length; j++)
                {
                    ExcelRange dCell = excelWorkSheet.Cells[i + 2, j + 1];
                    dCell.Value = pipeData[j];
                    dCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
            }

            //保存
            package.Save();
            package.Dispose();

            return Result.Succeeded;
        }

        string GetPipeSys(string abbreviation)
        {
            Dictionary<string, string> sysDic = new Dictionary<string, string>();
            sysDic.Add("ZP", "消防系统");
            sysDic.Add("X", "消防系统");
            sysDic.Add("J", "给水系统");
            sysDic.Add("F", "排水系统");
            sysDic.Add("W", "排水系统");

            return sysDic[abbreviation];
        }

        string GetPipeMaterial(double pipeSize, string abbreviation)
        {
            string material = "未定义";
            switch (abbreviation)
            {
                case "ZP":
                    material = "镀锌钢管";
                    break;
                case "X":
                    material = "镀锌钢管";
                    break;
                case "J":
                    if (pipeSize > 50)
                    {
                        material = "钢塑复合管";
                    }
                    else
                    {
                        material = "PP-R管";
                    }
                    break;
                case "F":
                    material = "PVC-U管";
                    break;
                case "W":
                    material = "PVC-U管";
                    break;
            }
            return material;
        }

        string GetPipeConnect(double pipeSize, string material)
        {
            string connect = "未定义";
            switch (material)
            {
                case "PVC-U管":
                    connect = "粘接";
                    break;
                case "PP-R管":
                    connect = "热熔";
                    break;
                case "钢塑复合管":
                    if (pipeSize > 65)
                    {
                        connect = "卡箍";
                    }
                    else
                    {
                        connect = "螺纹";
                    }
                    break;
                case "镀锌钢管":
                    if (pipeSize > 65)
                    {
                        connect = "卡箍";
                    }
                    else
                    {
                        connect = "螺纹";
                    }
                    break;
            }
            return connect;
        }
    }
}

/*
————————————————
版权声明：本文为CSDN博主「imfour」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/imfour/article/details/82959945
*/