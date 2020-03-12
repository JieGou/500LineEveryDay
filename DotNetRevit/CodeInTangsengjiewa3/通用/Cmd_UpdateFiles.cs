using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.Win32;

namespace CodeInTangsengjiewa3.通用
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_UpdateFiles : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Application app = commandData.Application.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acView = uidoc.ActiveView;

            OpenFileDialog opdg = new OpenFileDialog();
            opdg.Filter = "(*.rfa)|*.rfa|(*.rvt)|*.rvt";
            opdg.Multiselect = true;
            opdg.FileOk += OnFileOK;
            var dialogResult = opdg.ShowDialog();
            var count = opdg.FileNames.Length;
            string[] files = new string[count];
            if (dialogResult == true)
            {
                files = opdg.FileNames;
            }
            foreach (var file in files)
            {
                var temDoc = app.OpenDocumentFile(file);
                temDoc.Save();
                temDoc.Close();
            }
            return Result.Succeeded;
        }

        private void OnFileOK(object sender, CancelEventArgs e)
        {
            (sender as OpenFileDialog).RestoreDirectory = true;
        }
    }
}