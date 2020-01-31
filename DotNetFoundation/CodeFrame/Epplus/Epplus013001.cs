using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;

namespace InsertValueIntoExcelWithEpplus
{
    class E013001
    {
        static void Main(string[] args)
        {
            //Excel文件路径
            string path = @"D:\TestDir1\test.xlsx";

            //如文件已存在则删除
            if (File.Exists(path)) File.Delete(path);

            //创建Excel文件
            
            ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path));

            ExcelWorksheet excelWorkSheet = excelPackage.Workbook.Worksheets.Add("测试表");

            using (excelPackage) //不写在using里也能执行.
            {
                //指定需要写入的sheet名
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets["测试表"];

                //比如修改第一行,第一列的值为 0
                excelWorksheet.Cells[1, 1].Value = 0;

                //修改第一行,第二列的值为 你好
                excelWorksheet.Cells[1, 2].Value = "你好";

                //然后保存即可
                excelPackage.Save();
            }
        }
    }
}