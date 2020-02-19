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
    class RevitDataToExcelDemo3 : IExternalCommand
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

            var str = document.PathName;

            TaskDialog.Show("tips", str);

            return Result.Succeeded;
        }
    }
}