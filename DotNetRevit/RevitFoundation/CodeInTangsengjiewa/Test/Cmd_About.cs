using System;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassTeacherXu.Extensions;
using CodeInTangsengjiewa.BinLibrary.RevitHelper;
using CodeInTangsengjiewa.Test.UIs;

namespace CodeInTangsengjiewa.Test
{
    /// <summary>
    /// 计算墙的两面面积
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    class Cmd_About : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            AboutForm form = new AboutForm();
            form.Show(RevitWindowhelper.GetRevitWindow());

            return Result.Succeeded;
        }
    }
}