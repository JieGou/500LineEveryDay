using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using WPFIExternalCommandDemo;


namespace WPFInRevitDemo
{
    [Transaction(TransactionMode.Manual)]
    class ShowWpfDemo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            MainWindow mainWindow = new MainWindow();
            //非模态窗体
            mainWindow.Show();

            Transaction ts = new Transaction(doc, "******");
            ts.Start();

            ts.Commit();

            return Result.Succeeded;
        }
    }
}