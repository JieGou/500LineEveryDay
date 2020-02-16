using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa.BinLibrary.Extensions;
using Color = System.Drawing.Color;

namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.通用
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_ChangeBackGroundColor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            ColorDialog colordialog = new ColorDialog();
            var colorResult = colordialog.ShowDialog();
            var targetColor = default(Color);

            if (colorResult == DialogResult.OK)
            {
                targetColor = colordialog.Color;
            }
            else
            {
                return Result.Cancelled;
            }

            uiapp.Application.BackgroundColor =
                targetColor.ToRvtColor(); // uiapp.Application.BackgroundColor.InversColor();

            return Result.Succeeded;
        }
    }
}