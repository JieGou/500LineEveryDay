using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using System.ComponentModel;

namespace CodeInTangsengjiewa2.通用
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_UpdateFiles : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var dbapp = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = uidoc.ActiveView;

            var filePath = default(string);

            OpenFileDialog opdg = new OpenFileDialog();
            opdg.Filter = "(*.rfa)|*.rfa|(*.rvt)|*.rvt";

            opdg.Multiselect = true;

            opdg.FileOk += OnfileOk;

            var dialogresult = opdg.ShowDialog();

            var count = opdg.FileNames.Length;

            string[] files = new string[count];
            if (dialogresult == true)
            {
                files = opdg.FileNames;
            }

            foreach (var file in files)
            {
                var temdoc = dbapp.OpenDocumentFile(file);
                temdoc.Save();
                temdoc.Close();
            }
            return Result.Succeeded;
        }

        private void OnfileOk(object sender, CancelEventArgs e)
        {
            (sender as OpenFileDialog).RestoreDirectory = true;
        }
    }
}