using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;
using System.Threading.Tasks;

namespace EpplusSamples
{
    /// <summary>
    /// simply opens an existing file and reads some values and properties
    /// </summary>
    class E1001Samples02
    {
        public static void RunSample2(string FilePath)
        {
            Console.WriteLine("Reading column 2 of {0}", FilePath);

            Console.WriteLine();

            FileInfo existingFile = new FileInfo(FilePath);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int col = 2; //the item description

                //output the data in column 2
                for (int row = 0; row < 5; row++)
                {
                    Console.WriteLine("\tCell({0},{1}).Value={2}", row, col, worksheet.Cells[row, col].Value);
                }

                //output the formula in row 5
                Console.WriteLine("\tCell({0},{1}).Formula={2}", 3, 5, worksheet.Cells[3, 5].Formula);
                Console.WriteLine("\tCell({0},{1}).FormulaR1C1={2}", 3, 5, worksheet.Cells[3, 5].FormulaR1C1);

                // output the formula in row 5
                Console.WriteLine("\tCell({0},{1}).Formula={2}", 5, 3, worksheet.Cells[5, 3].Formula);
                Console.WriteLine("\tCell({0},{1}).FormulaR1C1={2}", 5, 3, worksheet.Cells[5, 3].FormulaR1C1);
            }
            //the using statement aotomatically calls Dispose() which closes the package
            // reivt的事物也继承了IDisposable接口 public class Transaction : IDisposable

            Console.WriteLine();
            Console.WriteLine("Sample 2 complete");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}