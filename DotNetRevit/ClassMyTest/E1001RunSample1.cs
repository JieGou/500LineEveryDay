using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;
using System.Xml;
using System.Drawing;
using OfficeOpenXml.Style;


namespace Myclass
{
   public class RunEpplusSample
    {
        /// <summary>
        ///
        /// 例子1 简单的从0创建一个新的工作簿
        /// 工作簿包含一张工作表,简单的财产清单
        /// </summary>
        public static void RunSample1()
        {
            using (var package = new ExcelPackage(new FileInfo(@"d:\epplusSample01.xlsx")))

            {
                //添加一张新的工作表进入工作薄
                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Inventory");
                //添加表头
                worksheet1.Cells[1, 1].Value = "ID";
                worksheet1.Cells[1, 2].Value = "Product";
                worksheet1.Cells[1, 3].Value = "Quantity";
                worksheet1.Cells[1, 4].Value = "Price";
                worksheet1.Cells[1, 5].Value = "Value";

                //添加一些内容
                worksheet1.Cells["A2"].Value = 12001;
                worksheet1.Cells["B2"].Value = "Nails";
                worksheet1.Cells["C2"].Value = 37;
                worksheet1.Cells["D2"].Value = 3.99;

                worksheet1.Cells["A3"].Value = 12002;
                worksheet1.Cells["B3"].Value = "Hammer";
                worksheet1.Cells["C3"].Value = 5;
                worksheet1.Cells["D3"].Value = 12;

                worksheet1.Cells["A4"].Value = 12003;
                worksheet1.Cells["B4"].Value = "Saw";
                worksheet1.Cells["C4"].Value = 10;
                worksheet1.Cells["D4"].Value = 100;

                //添加一个公式放在value列里.
                worksheet1.Cells["E2:E4"].Formula = "C2*D2";

                //好,现在格式化values
                using (var range = worksheet1.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                worksheet1.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells["A5:E5"].Style.Font.Bold = true;

                worksheet1.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0}",
                    new ExcelAddress(2, 3, 4, 3).Address);
                worksheet1.Cells["C2:C5"].Style.Numberformat.Format = "#.##0";
                worksheet1.Cells["D2:E5"].Style.Numberformat.Format = "#.##0.00";

                //创建一个自动过滤器 为range
                worksheet1.Cells["A1:E4"].AutoFilter = true;
                worksheet1.Cells["D2:E5"].Style.Numberformat.Format = "@"; //文本格式

                //实际上没有必要计算,excel会自动完成计算. 但有时候特殊情况,需要算一下
                //比如,你链接这张表格到另外的表格,或者你打开这张表格的软件没有计算引擎
                //再比如,你想用公式的计算结果到你的程序中

                //worksheet1.Calculate();
                worksheet1.Cells.AutoFitColumns(); //自动适应列宽

                //设置表头的文字
                worksheet1.HeaderFooter.OddFooter.CenteredText = "&23&U\"Arial,Regular Bold\"Inventory";

                //添加页码到 footer plus the total numbers of pages
                worksheet1.HeaderFooter.OddFooter.RightAlignedText = string.Format("Page {0} of {1}",
                    ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                //添加sheet name to the footer
                worksheet1.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;

                //添加file path to the footer
                worksheet1.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath +
                                                                    ExcelHeaderFooter.FileName;

                worksheet1.PrinterSettings.RepeatRows = worksheet1.Cells["1:2"];
                worksheet1.PrinterSettings.RepeatColumns = worksheet1.Cells["A:G"];

                //改变工作表的 view to show it in page layout mode
                worksheet1.View.PageLayoutView = true;

                //设置一些文档属性
                package.Workbook.Properties.Title = "Inventory";
                package.Workbook.Properties.Author = "老王";
                package.Workbook.Properties.Comments = "this sample demonstrates how to " +
                                                       "create an Excel 2007 workbook using EPPlus";

                //设置一些扩展属性值
                package.Workbook.Properties.Company = "AdventureWorks Inc";

                //设置一些custom 属性值
                package.Workbook.Properties.SetCustomPropertyValue("checked by", "老往");
                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                
                //save our new workbook in the output directory an we are done!
                package.Save();
                
            }

        }
    }
}